using Refactoring.FraudDetection.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Refactoring.FraudDetection.OrderProviders
{
    public class FromFileOrderProvider : BaseOrderProvider
    {
        public FromFileOrderProvider(string filePath)
        {
            this.filePath = filePath;
        }

        protected override async Task<IEnumerable<Order>> GetOrdersFromSource()
        {
            var textLines = await File.ReadAllLinesAsync(filePath);

            return textLines.Select(Order.Parse);
        }

        private readonly string filePath;
    }
}
