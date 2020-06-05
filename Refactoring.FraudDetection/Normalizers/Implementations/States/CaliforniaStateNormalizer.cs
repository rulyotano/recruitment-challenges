namespace Refactoring.FraudDetection.Normalizers.Implementations.States
{
    public class CaliforniaStateNormalizer : StateNormalizerBase
    {
        public const string CALIFORNIA_SHORT = "ca";
        public const string CALIFORNIA_LONG = "california";

        public CaliforniaStateNormalizer()
            : base(CALIFORNIA_SHORT, CALIFORNIA_LONG)
        {
        }
    }
}
