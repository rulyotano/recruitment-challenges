using Refactoring.FraudDetection.Models.Addresses;
using Refactoring.FraudDetection.Normalizers;
using Refactoring.FraudDetection.Normalizers.Common;
using System;

namespace Refactoring.FraudDetection.Models
{
    public class Order
    {
        public int OrderId { get; protected set; }

        public int DealId { get; protected set; }

        public string Email
        {
            get => email;
            set
            {
                if (normalizerProvider != null)
                {
                    email = ValidateEmailAddress(normalizerProvider
                        .GetNormalizers(it => it is ICommonNormalizer || it is IEmailNormalizer)
                        .NormalizeAll(value));
                }
                else
                {
                    email = ValidateEmailAddress(value);
                }
            }
        }

        public Address Address { get; set; }

        public string CreditCard { get; set; }

        public static Order Parse(string s)
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

        public static void UseNormalizers(INormalizerProvider normalizerProvider)
        {
            Order.normalizerProvider = normalizerProvider;
        }

        public Order(int orderId, int dealId, string email, Address address, string creditCard)
        {
            OrderId = orderId;
            DealId = dealId;
            Email = email;
            Address = address;
            CreditCard = creditCard;
        }

        private string ValidateEmailAddress(string inputArgument)
        {
            try
            {
                var parsedEmailAddress = new System.Net.Mail.MailAddress(inputArgument);
                return parsedEmailAddress.Address;
            }
            catch (Exception)
            {
                throw new ArgumentException(PARSE_INVALID_EMAIL_EXCEPTION_TEXT);
            }
        }

        private string email;
        private static INormalizerProvider normalizerProvider;
        private const string PARSE_NULL_STRING_EXCEPTION_TEXT = "String to be parsed is null";
        private const string PARSE_INCORRECT_LENGHT_EXCEPTION_TEXT = "Input do not have the correct format. Data is missing.";
        private const string PARSE_DEAL_ID_NO_INT_EXCEPTION_TEXT = "Deal Id should be a number";
        private const string PARSE_ORDER_ID_NO_INT_EXCEPTION_TEXT = "Order Id should be a number";
        private const string PARSE_INVALID_EMAIL_EXCEPTION_TEXT = "Invalid email address";
    }
}
