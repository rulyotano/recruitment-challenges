using Refactoring.FraudDetection.Normalizers.Implementations.Emails;
using Refactoring.FraudDetection.Normalizers.Implementations.States;
using Refactoring.FraudDetection.Normalizers.Implementations.Streets;

namespace Refactoring.FraudDetection.Normalizers
{
    public static class NormalizerProvider
    {
        public static INormalizerProvider Current { get; set; }
            = new DefaultNormalizerProvider();
    }

    public class DefaultNormalizerProvider : ParameterizedNormalizerProvider
    {
        public DefaultNormalizerProvider()
            : base(new INormalizer[]
            {
                new ToLowerNormalizer(),
                new TrimNormalizer(),
                new EmailNormalizer(),
                new CaliforniaStateNormalizer(),
                new IllinoisStateNormalizer(),
                new NewYorkStateNormalizer(),
                new StreetNormalizer()
            })
        { }
    }
}
