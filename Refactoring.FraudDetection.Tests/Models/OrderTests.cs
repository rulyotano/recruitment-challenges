using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Models;
using Refactoring.FraudDetection.Models.Addresses;
using Refactoring.FraudDetection.Normalizers;
using Refactoring.FraudDetection.Tests.Normalizers;
using System;

namespace Refactoring.FraudDetection.Tests.Models
{
    [TestClass]
    public class OrderTests
    {
        [TestCleanup]
        public void CleanTests()
        {
            Order.UseNormalizers(null);
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
            Order.UseNormalizers(GetNormalizerProvider());

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
            Order.UseNormalizers(GetNormalizerProvider());

            order.Email = invalidEmailAddress;
        }
        #endregion

        #region Parse

        [TestMethod]
        public void Parse_WithValidFormatShould_ReturnOrderWithAllFields()
        {
            var order = Order.Parse($"{FAKE_ORDER_ID},{FAKE_DEAL_ID},{FAKE_EMAIL},{FAKE_ADDRESS.Street},{FAKE_ADDRESS.City},{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode},{FAKE_CARD}");

            order.OrderId.Should().Be(FAKE_ORDER_ID);
            order.DealId.Should().Be(FAKE_DEAL_ID);
            order.Email.Should().Be(FAKE_EMAIL);
            order.Address.Street.Should().Be(FAKE_ADDRESS.Street);
            order.Address.City.Should().Be(FAKE_ADDRESS.City);
            order.Address.State.Should().Be(FAKE_ADDRESS.State);
            order.Address.ZipCode.Should().Be(FAKE_ADDRESS.ZipCode);
            order.CreditCard.Should().Be(FAKE_CARD);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WithNullString_ShouldRaiseArgumentException()
        {
            Order.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WithEmptyString_ShouldRaiseArgumentException()
        {
            Order.Parse("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WithLessFields_ShouldRaiseArgumentException()
        {
            Order.Parse($"{FAKE_ORDER_ID},{FAKE_DEAL_ID},{FAKE_EMAIL},{FAKE_ADDRESS.Street},{FAKE_ADDRESS.City},{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode}");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WithEmptyFied_ShouldRaiseArgumentException()
        {
            Order.Parse($"{FAKE_ORDER_ID},{FAKE_DEAL_ID},{FAKE_EMAIL},{FAKE_ADDRESS.Street},,{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode},{FAKE_CARD}");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WithNoNumberOrderId_RaiseArgumentException()
        {
            Order.Parse($"orderId,{FAKE_DEAL_ID},{FAKE_EMAIL},{FAKE_ADDRESS.Street},{FAKE_ADDRESS.City},{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode},{FAKE_CARD}");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WithNoNumberDeailId_RaiseArgumentException()
        {
            Order.Parse($"{FAKE_ORDER_ID},dealId,{FAKE_EMAIL},{FAKE_ADDRESS.Street},{FAKE_ADDRESS.City},{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode},{FAKE_CARD}");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("wrong@.address")]
        [DataRow("wrong_address")]
        public void Parse_WithInvalidEmail_RaiseArgumentException(string wrongAddress)
        {
            Order.Parse($"{FAKE_ORDER_ID},{FAKE_DEAL_ID},{wrongAddress},{FAKE_ADDRESS.Street},{FAKE_ADDRESS.City},{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode},{FAKE_CARD}");
        }

        #endregion

        private static INormalizerProvider GetNormalizerProvider()
        {
            return new BasicNormalizerProvider(NormalizerTestHelpers.GetFakeNormalizers());
        }

        private static Order BuildOrder(int orderId = FAKE_ORDER_ID, int dealId = FAKE_DEAL_ID,
            string email = FAKE_EMAIL, Address address = null, string card = FAKE_CARD)
        {
            var addressArgument = address ?? FAKE_ADDRESS;
            return new Order(orderId, dealId, email, addressArgument, card);
        }

        private const int FAKE_ORDER_ID = 5;
        private const int FAKE_DEAL_ID = 6;
        private const string FAKE_EMAIL = "fake@email.com";
        private const string FAKE_CARD = "fake-card";
        private const string CUSTOM_VALUE = "custom-value";
        private static readonly Address FAKE_ADDRESS = new Address("fake-street", "fake-city", "fake-state", "fake-zip-code");
    }
}
