using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refactoring.FraudDetection.Models;
using Refactoring.FraudDetection.Normalizers;
using Refactoring.FraudDetection.Tests.Normalizers;
using System;

namespace Refactoring.FraudDetection.Tests.Models
{
    [TestClass]
    public class OrderTests
    {
        [TestInitialize]
        public void InitializeTests()
        {
            orderParserMock = new Mock<IOrderParser>();
            orderParserMock.Setup(it => it.Parse(It.IsAny<string>()))
                .Returns(ParsedOrder);
            OrderParser.Current = orderParserMock.Object;
            NormalizerProvider.Current = null;
        }

        [TestCleanup]
        public void CleanTests()
        {
            OrderParser.Current = new DefaultOrderParser();
            NormalizerProvider.Current = new DefaultNormalizerProvider();
        }

        #region Constructors
        [TestMethod]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            var order = BuildOrder();

            Assert.AreEqual(
                expected: FAKE_ORDER_ID,
                actual: order.OrderId,
                message: "Order Id is not correctly setted");

            Assert.AreEqual(
                expected: FAKE_DEAL_ID,
                actual: order.DealId,
                message: "Deal Id is not correctly setted");

            Assert.AreEqual(
                expected: FAKE_EMAIL,
                actual: order.Email,
                message: "Email is not correctly setted");

            Assert.AreEqual(
                expected: FAKE_ADDRESS,
                actual: order.Address,
                message: "Address is not correctly setted");

            Assert.AreEqual(
                expected: FAKE_CARD,
                actual: order.CreditCard,
                message: "Card is not correctly setted");
        }
        #endregion

        #region Email Set

        [TestMethod]
        public void SetEmail_WithNoNormalizer_ShouldSetValue()
        {
            var order = BuildOrder();

            order.Email = FAKE_EMAIL;

            order.Email.Should().Be(FAKE_EMAIL);
        }

        [TestMethod]
        public void SetEmail_WithNormalizer_ShouldSetValueAfterNormalize_WithCommonAndEmail()
        {
            var order = BuildOrder();
            NormalizerProvider.Current = GetNormalizerProvider();

            order.Email = FAKE_EMAIL;

            order.Email.Should().Contain(FAKE_EMAIL)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND1)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND2)
                .And.Contain(NormalizerTestHelpers.EMAIL_NORMALIZER_APPEND1)
                .And.Contain(NormalizerTestHelpers.EMAIL_NORMALIZER_APPEND2)
                .And.NotContain(NormalizerTestHelpers.STATE_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.STATE_NORMALIZER_APPEND2)
                .And.NotContain(NormalizerTestHelpers.STREET_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.STREET_NORMALIZER_APPEND2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("wrong_address")]
        [DataRow("wrong@.address")]
        public void SetEmail_WithInvalidEmailAddress_RaiseArgumentException(string invalidEmailAddress)
        {
            var order = BuildOrder();

            order.Email = invalidEmailAddress;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("wrong_address")]
        [DataRow("wrong@.address")]
        public void SetEmail_WithInvalidEmailAddress_WithNormalizer_RaiseArgumentException(string invalidEmailAddress)
        {
            var order = BuildOrder();
            NormalizerProvider.Current = GetNormalizerProvider();

            order.Email = invalidEmailAddress;
        }
        #endregion

        #region Card Set

        [TestMethod]
        public void SetCard_WithNoNormalizer_ShouldSetValue()
        {
            var order = BuildOrder();

            order.CreditCard = FAKE_EMAIL;

            order.CreditCard.Should().Be(FAKE_EMAIL);
        }

        [TestMethod]
        public void SetCard_WithNormalizer_ShouldSetValueAfterNormalize_WithCommonAndEmail()
        {
            var order = BuildOrder();
            NormalizerProvider.Current = GetNormalizerProvider();

            order.CreditCard = FAKE_EMAIL;

            order.CreditCard.Should().Contain(FAKE_EMAIL)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND1)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND2)
                .And.NotContain(NormalizerTestHelpers.EMAIL_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.EMAIL_NORMALIZER_APPEND2)
                .And.NotContain(NormalizerTestHelpers.STATE_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.STATE_NORMALIZER_APPEND2)
                .And.NotContain(NormalizerTestHelpers.STREET_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.STREET_NORMALIZER_APPEND2);
        }

        #endregion

        #region Parse

        [TestMethod]
        public void Parse_ShouldCallStaticOrderParserCurrent()
        {
            var parserInput = $"{FAKE_ORDER_ID},{FAKE_DEAL_ID},{FAKE_EMAIL},{FAKE_ADDRESS.Street},{FAKE_ADDRESS.City},{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode},{FAKE_CARD}";
            var order = Order.Parse(parserInput);

            orderParserMock.Verify(it => it.Parse(parserInput));
            order.Should().Be(ParsedOrder);
        }

        #endregion

        private static INormalizerProvider GetNormalizerProvider()
        {
            return new ParameterizedNormalizerProvider(NormalizerTestHelpers.GetFakeNormalizers());
        }

        private static Order BuildOrder(int orderId = FAKE_ORDER_ID, int dealId = FAKE_DEAL_ID,
            string email = FAKE_EMAIL, Address address = null, string card = FAKE_CARD)
        {
            var addressArgument = address ?? FAKE_ADDRESS;
            return new Order(orderId, dealId, email, addressArgument, card);
        }

        private Order ParsedOrder = BuildOrder(orderId: PARSED_ORDER_ID);
        private Mock<IOrderParser> orderParserMock;
        private const int PARSED_ORDER_ID = 33;
        private const int FAKE_ORDER_ID = 5;
        private const int FAKE_DEAL_ID = 6;
        private const string FAKE_EMAIL = "fake@email.com";
        private const string FAKE_CARD = "fake-card";
        private static readonly Address FAKE_ADDRESS = new Address("fake-street", "fake-city", "fake-state", "fake-zip-code");
    }
}
