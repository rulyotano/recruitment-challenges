using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Models;
using Refactoring.FraudDetection.OrderProviders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Refactoring.FraudDetection.Tests.OrderProviders
{
    [TestClass]
    public class BaseOrderProviderTests
    {
        [TestMethod]
        public async Task GetOrders_ShouldReturnDictionaryWhenNoRepeatedId()
        {
            var provider = new TestBaseOrderProvider(TestCases.FourLinesMoreTanOneFraud);

            var result = await provider.GetOrdersAsync();

            result.Count.Should().Be(4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetOrders_WhenRepeatedId_ShouldRaiseArgumentException()
        {
            var provider = new TestBaseOrderProvider(TestCases.ThreeLinesRepeatedId);

            await provider.GetOrdersAsync();
        }
    }

    public class TestBaseOrderProvider : BaseOrderProvider
    {
        private readonly IEnumerable<Order> testOrders;

        protected override async Task<IEnumerable<Order>> GetOrdersFromSource()
        {
            return testOrders;
        }

        public TestBaseOrderProvider(IEnumerable<Order> testOrders)
        {
            this.testOrders = testOrders;
        }
    }
}
