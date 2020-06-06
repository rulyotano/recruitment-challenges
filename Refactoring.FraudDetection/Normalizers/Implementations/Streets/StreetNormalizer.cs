using Refactoring.FraudDetection.Normalizers.Common;

namespace Refactoring.FraudDetection.Normalizers.Implementations.Streets
{
    public class StreetNormalizer : IStreetNormalizer
    {
        public string Normalize(string test)
        {
            return test
                .ReplaceWithPattern(@"\bst\.", "street")
                .ReplaceWithPattern(@"\brd\.", "road");
        }
    }
}
