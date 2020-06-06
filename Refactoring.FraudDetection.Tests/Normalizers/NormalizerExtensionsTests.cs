using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Normalizers;
using Refactoring.FraudDetection.Normalizers.Common;
using System.Linq;

namespace Refactoring.FraudDetection.Tests.Normalizers
{
    [TestClass]
    public class NormalizerExtensionsTests
    {
        #region Normalize
        [TestMethod]
        public void NormalizeAll_ShouldApplyAllPassedNormalizers()
        {
            var provider = BuildBasicNormalizerProvider();

            var commonNormalizers = provider.GetNormalizers(it => it is ICommonNormalizer);

            var result = commonNormalizers.NormalizeAll(BASE_VALUE);

            result.Should().Contain(BASE_VALUE)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND1)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND2);
        }

        #endregion

        private static ParameterizedNormalizerProvider BuildBasicNormalizerProvider()
        {
            return new ParameterizedNormalizerProvider(NormalizerTestHelpers.GetFakeNormalizers());
        }

        private const string BASE_VALUE = "base-value";
    }
}
