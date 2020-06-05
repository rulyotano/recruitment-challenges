using Refactoring.FraudDetection.Normalizers.Common;
using System;

namespace Refactoring.FraudDetection.Normalizers.Implementations.Emails
{
    public class EmailNormalizer : IEmailNormalizer
    {
        public string Normalize(string emailText)
        {
            var aux = emailText.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
