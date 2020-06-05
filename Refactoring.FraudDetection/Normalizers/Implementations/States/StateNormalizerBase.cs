using Refactoring.FraudDetection.Normalizers.Common;

namespace Refactoring.FraudDetection.Normalizers.Implementations.States
{
    public abstract class StateNormalizerBase : IStateNormalizer
    {
        public string ShortName => shortName;

        public string CompleteName => completeName;

        public string Normalize(string emailText)
        {
            return emailText.ReplaceWithPattern($@"\b{ShortName}\b", CompleteName);
        }

        public StateNormalizerBase(string shortName, string completeName)
        {
            this.shortName = shortName;
            this.completeName = completeName;
        }

        private readonly string shortName;
        private readonly string completeName;
    }
}
