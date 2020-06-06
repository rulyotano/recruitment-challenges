using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.OrderProviders;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Refactoring.FraudDetection.Tests.OrderProviders
{
    [TestClass]
    public class FromFileOrderProviderTests
    {
        [TestMethod]
        [DeploymentItem("./Files/SixLines_FraudulentMultiples_SameAddressAfterNormalized.txt", "Files")]
        public async Task GetOrders_ShouldReturnDictionaryWhenNoRepeatedId()
        {
            var provider = new FromFileOrderProvider(Path.Combine(Environment.CurrentDirectory, "Files", "SixLines_FraudulentMultiples_SameAddressAfterNormalized.txt"));

            var result = await  provider.GetOrdersAsync();

            result.Count.Should().Be(6);
        }
    }
}
