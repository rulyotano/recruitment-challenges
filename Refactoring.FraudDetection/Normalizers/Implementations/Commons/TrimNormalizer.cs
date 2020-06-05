using Refactoring.FraudDetection.Normalizers.Common;

namespace Refactoring.FraudDetection.Normalizers.Implementations.Emails
{
    public class TrimNormalizer : ICommonNormalizer
    {
        public string Normalize(string text)
        {
            return text.Trim();
        }
    }
}
