using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Refactoring.FraudDetection.Normalizers
{
    public class ParameterizedNormalizerProvider : INormalizerProvider
    {
        public IEnumerable<INormalizer> GetNormalizers(Expression<Func<INormalizer, bool>> filter = null)
        {
            if (filter != null)
            {
                return normalizers.Where(filter.Compile());
            }
            return normalizers;
        }

        public ParameterizedNormalizerProvider(IEnumerable<INormalizer> normalizers)
        {
            this.normalizers = normalizers;
        }

        private readonly IEnumerable<INormalizer> normalizers;
    }
}
