using Moq;
using Refactoring.FraudDetection.Normalizers;
using Refactoring.FraudDetection.Normalizers.Common;
using System;
using System.Collections.Generic;

namespace Refactoring.FraudDetection.Tests.Normalizers
{
    public static class NormalizerTestHelpers
    {
        public const string COMMON_NORMALIZER_APPEND1 = ".common1";
        public const string COMMON_NORMALIZER_APPEND2 = ".common2";
        public const string STREET_NORMALIZER_APPEND1 = ".street1";
        public const string STREET_NORMALIZER_APPEND2 = ".street2";
        public const string STATE_NORMALIZER_APPEND1 = ".state1";
        public const string STATE_NORMALIZER_APPEND2 = ".state2";
        public const string EMAIL_NORMALIZER_APPEND1 = ".email1";
        public const string EMAIL_NORMALIZER_APPEND2 = ".email2";

        public static IEnumerable<INormalizer> GetFakeNormalizers()
        {
            return new List<INormalizer> {
                GetNormalizerMock<ICommonNormalizer>(COMMON_NORMALIZER_APPEND1).Object,
                GetNormalizerMock<ICommonNormalizer>(COMMON_NORMALIZER_APPEND2).Object,
                GetNormalizerMock<IStreetNormalizer>(STREET_NORMALIZER_APPEND1).Object,
                GetNormalizerMock<IStreetNormalizer>(STREET_NORMALIZER_APPEND2).Object,
                GetNormalizerMock<IStateNormalizer>(STATE_NORMALIZER_APPEND1).Object,
                GetNormalizerMock<IStateNormalizer>(STATE_NORMALIZER_APPEND2).Object,
                GetNormalizerMock<IEmailNormalizer>(EMAIL_NORMALIZER_APPEND1).Object,
                GetNormalizerMock<IEmailNormalizer>(EMAIL_NORMALIZER_APPEND2).Object
            };
        }

        private static Mock<T> GetNormalizerMock<T>(string append) where T : class, INormalizer
        {
            var mock = new Mock<T>();
            mock.Setup(it => it.Normalize(It.IsAny<string>()))
                .Returns(new Func<string, string>(s => $"{s}.{append}"));
            return mock;
        }
    }
}
