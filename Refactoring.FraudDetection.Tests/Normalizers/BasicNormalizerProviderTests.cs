using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Normalizers;
using Refactoring.FraudDetection.Normalizers.Common;
using System.Linq;

namespace Refactoring.FraudDetection.Tests.Normalizers
{
    [TestClass]
    public class BasicNormalizerProviderTests
    {
        #region Normalize
        [TestMethod]
        public void Normalize_WhenNoFilter_ShouldReturnAllNormalizers()
        {
            var provider = BuildBasicNormalizerProvider();

            var result = provider.GetNormalizers();

            result.Should().HaveCount(NormalizerTestHelpers.GetFakeNormalizers().Count(), because: "when no filter should return all normalizers");
        }

        [TestMethod]
        public void Normalize_FilterByStreet_ShouldOnlyReturnStreetNormalizers()
        {
            var provider = BuildBasicNormalizerProvider();

            var result = provider.GetNormalizers(it => it is IStreetNormalizer);

            result.Should().AllBeAssignableTo<IStreetNormalizer>(because: "when filter with street, should only return street normalizers");
        }

        [TestMethod]
        public void Normalize_FilterByStateOrCommon_ShouldOnlyReturnStateAndCommonNormalizers()
        {
            var provider = BuildBasicNormalizerProvider();

            var result = provider.GetNormalizers(it => it is IStateNormalizer || it is ICommonNormalizer);

            result.Should().Match(r => r.All(it => it is IStateNormalizer || it is ICommonNormalizer),
                because: "when filter with state or common, should only return state or common");
        }
        #endregion

        private static BasicNormalizerProvider BuildBasicNormalizerProvider()
        {
            return new BasicNormalizerProvider(NormalizerTestHelpers.GetFakeNormalizers());
        }
    }
}
