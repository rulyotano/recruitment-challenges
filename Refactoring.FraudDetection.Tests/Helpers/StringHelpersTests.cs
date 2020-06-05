using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Normalizers;
using Refactoring.FraudDetection.Normalizers;

namespace Refactoring.FraudDetection.Tests.Helpers
{
    [TestClass]
    public class StringHelpersTests
    {
        [DataTestMethod]
        [DataRow(@"\bil\b", "illinois", "illinois", "illinois", "word with prefix should not be replaced (illinois)")]
        [DataRow(@"\bca\b", "california", "california", "california", "word with prefix should not be replaced (california)")]
        [DataRow(@"\bil\b", "illinois", "il", "illinois", "word matching pattern should be replaced (illinois)")]
        [DataRow(@"\bca\b", "california", "ca", "california", "word matching pattern should be replaced (california)")]
        [DataRow(@"\bca\b", "california", "at ca came", "at california came", "substring matching pattern should be replaced")]
        public void ReplaceWithPatter_ShouldReplaceOnlyWhenMatching(string pattern, string replaceWith,
            string stringToReplace, string expected, string description)
        {
            var result = stringToReplace.ReplaceWithPattern(pattern, replaceWith);

            Assert.AreEqual(
                expected: expected,
                actual: result,
                message: description);
        }
    }
}
