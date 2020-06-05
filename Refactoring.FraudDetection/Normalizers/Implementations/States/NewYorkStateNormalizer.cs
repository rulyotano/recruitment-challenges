namespace Refactoring.FraudDetection.Normalizers.Implementations.States
{
    public class NewYorkStateNormalizer : StateNormalizerBase
    {
        public const string NEW_YORK_SHORT = "ny";
        public const string NEW_YORK_LONG = "new york";

        public NewYorkStateNormalizer()
            : base(NEW_YORK_SHORT, NEW_YORK_LONG)
        {
        }
    }
}
