using System.Collections.Generic;

namespace Refactoring.FraudDetection.Normalizers
{
    public static class NormalizerExtensions
    {
        public static string NormalizeAll(this IEnumerable<INormalizer> normalizers, string textToNormalize)
        {
            var currentText = textToNormalize;
            foreach (var normalizer in normalizers)
            {
                currentText = normalizer.Normalize(currentText);
            }

            return currentText;
        }
    }
}
