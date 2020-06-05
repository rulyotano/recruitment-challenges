using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Refactoring.FraudDetection.Normalizers
{
    public interface INormalizerProvider
    {
        IEnumerable<INormalizer> GetNormalizers(Expression<Func<INormalizer, bool>> filter = null);
    }
}
