using Refactoring.FraudDetection.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Refactoring.FraudDetection.OrderProviders
{
    public interface IOrderProvider
    {
        Task<IDictionary<int, Order>> GetOrdersAsync();
    }
}
