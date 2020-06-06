using Refactoring.FraudDetection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refactoring.FraudDetection.OrderProviders
{
    public abstract class BaseOrderProvider : IOrderProvider
    {
        public async Task<IDictionary<int, Order>> GetOrdersAsync()
        {
            var sourcers = await GetOrdersFromSource();

            return GetOrdersById(sourcers);
        }

        protected IDictionary<int, Order> GetOrdersById(IEnumerable<Order> orders)
        {
            try
            {
                return orders.ToDictionary(it => it.OrderId);
            }
            catch (Exception)
            {
                throw new ArgumentException("Orders should have unique order ids.");
            }
        }

        protected abstract Task<IEnumerable<Order>> GetOrdersFromSource();
    }
}
