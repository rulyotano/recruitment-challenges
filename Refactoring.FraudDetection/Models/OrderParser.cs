using Refactoring.FraudDetection.Models.Addresses;
using System;

namespace Refactoring.FraudDetection.Models
{
    public static class OrderParser
    {
        public static IOrderParser Current { get; set; } = new DefaultOrderParser();
    }

    public class DefaultOrderParser : IOrderParser
    {
        public Order Parse(string s)
        {
            if (s == null)
                throw new ArgumentException(PARSE_NULL_STRING_EXCEPTION_TEXT);

            var items = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (items.Length < 8)
                throw new ArgumentException(PARSE_INCORRECT_LENGHT_EXCEPTION_TEXT);

            if (!int.TryParse(items[0], out int dealId))
                throw new ArgumentException(PARSE_DEAL_ID_NO_INT_EXCEPTION_TEXT);

            if (!int.TryParse(items[1], out int orderId))
                throw new ArgumentException(PARSE_ORDER_ID_NO_INT_EXCEPTION_TEXT);

            var address = new Address(items[3], items[4], items[5], items[6]);

            return new Order(dealId, orderId, items[2], address, items[7]);
        }

        private const string PARSE_NULL_STRING_EXCEPTION_TEXT = "String to be parsed is null";
        private const string PARSE_INCORRECT_LENGHT_EXCEPTION_TEXT = "Input do not have the correct format. Data is missing.";
        private const string PARSE_DEAL_ID_NO_INT_EXCEPTION_TEXT = "Deal Id should be a number";
        private const string PARSE_ORDER_ID_NO_INT_EXCEPTION_TEXT = "Order Id should be a number";
    }
}
