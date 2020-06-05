using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Models;

namespace Refactoring.FraudDetection.Tests.Models
{
    [TestClass]
    public class FraudResultTests
    {
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void Constructor_ShouldSetPropertiesCorrectly(bool isFraudulent)
        {
            var fraudResult = new FraudResult(FAKE_ORDER_ID, isFraudulent);

            Assert.AreEqual(
                expected: FAKE_ORDER_ID,
                actual: fraudResult.OrderId,
                message: "Order Id is not correctly setted");

            Assert.AreEqual(
                expected: isFraudulent,
                actual: fraudResult.IsFraudulent,
                message: "Is Fraudulent is not correctly setted");
        }

        private const int FAKE_ORDER_ID = 5;
    }
}
