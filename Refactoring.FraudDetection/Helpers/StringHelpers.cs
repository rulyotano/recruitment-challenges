using System.Text.RegularExpressions;

namespace Refactoring.FraudDetection.Normalizers
{
    public static class StringHelpers
    {
        public static string ReplaceWithPattern(this string original, string pattern, string replace)
        {
            return Regex.Replace(original, pattern, replace);
        }
    }
}
