namespace Refactoring.FraudDetection.Normalizers.Implementations.States
{
    public class IllinoisStateNormalizer : StateNormalizerBase
    {
        public const string ILLINOIS_SHORT = "il";
        public const string ILLINOIS_LONG = "illinois";

        public IllinoisStateNormalizer()
            : base(ILLINOIS_SHORT, ILLINOIS_LONG)
        {
        }
    }
}
