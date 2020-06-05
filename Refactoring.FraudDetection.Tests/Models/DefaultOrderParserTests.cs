using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Models;
using Refactoring.FraudDetection.Models.Addresses;
using System;

namespace Refactoring.FraudDetection.Tests.Models
{
    [TestClass]
    public class DefaultOrderParserTests
    {
        #region Parse

        [TestMethod]
        public void Parse_WithValidFormatShould_ReturnOrderWithAllFields()
        {
            var order = GetDefaultOrderParser().Parse($"{FAKE_ORDER_ID},{FAKE_DEAL_ID},{FAKE_EMAIL},{FAKE_ADDRESS.Street},{FAKE_ADDRESS.City},{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode},{FAKE_CARD}");

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
            GetDefaultOrderParser().Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WithEmptyString_ShouldRaiseArgumentException()
        {
            GetDefaultOrderParser().Parse("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WithLessFields_ShouldRaiseArgumentException()
        {
            GetDefaultOrderParser().Parse($"{FAKE_ORDER_ID},{FAKE_DEAL_ID},{FAKE_EMAIL},{FAKE_ADDRESS.Street},{FAKE_ADDRESS.City},{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode}");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WithEmptyFied_ShouldRaiseArgumentException()
        {
            GetDefaultOrderParser().Parse($"{FAKE_ORDER_ID},{FAKE_DEAL_ID},{FAKE_EMAIL},{FAKE_ADDRESS.Street},,{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode},{FAKE_CARD}");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WithNoNumberOrderId_RaiseArgumentException()
        {
            GetDefaultOrderParser().Parse($"orderId,{FAKE_DEAL_ID},{FAKE_EMAIL},{FAKE_ADDRESS.Street},{FAKE_ADDRESS.City},{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode},{FAKE_CARD}");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WithNoNumberDeailId_RaiseArgumentException()
        {
            GetDefaultOrderParser().Parse($"{FAKE_ORDER_ID},dealId,{FAKE_EMAIL},{FAKE_ADDRESS.Street},{FAKE_ADDRESS.City},{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode},{FAKE_CARD}");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("wrong@.address")]
        [DataRow("wrong_address")]
        public void Parse_WithInvalidEmail_RaiseArgumentException(string wrongAddress)
        {
            GetDefaultOrderParser().Parse($"{FAKE_ORDER_ID},{FAKE_DEAL_ID},{wrongAddress},{FAKE_ADDRESS.Street},{FAKE_ADDRESS.City},{FAKE_ADDRESS.State},{FAKE_ADDRESS.ZipCode},{FAKE_CARD}");
        }

        #endregion

        private DefaultOrderParser GetDefaultOrderParser()
        {
            return new DefaultOrderParser();
        }

        private const int FAKE_ORDER_ID = 5;
        private const int FAKE_DEAL_ID = 6;
        private const string FAKE_EMAIL = "fake@email.com";
        private const string FAKE_CARD = "fake-card";
        private static readonly Address FAKE_ADDRESS = new Address("fake-street", "fake-city", "fake-state", "fake-zip-code");
    }
}
