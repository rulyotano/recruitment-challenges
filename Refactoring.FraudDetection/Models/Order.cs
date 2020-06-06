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
                var normalizerProvider = GetNormalizerProvider();
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

        public string CreditCard
        {
            get => creditCard;
            set
            {
                var normalizerProvider = GetNormalizerProvider();
                if (normalizerProvider != null)
                {
                    creditCard = normalizerProvider
                                    .GetNormalizers(it => it is ICommonNormalizer)
                                    .NormalizeAll(value);
                }
                else
                {
                    creditCard = value;
                }
            }
        }

        public static Order Parse(string s)
        {
            return OrderParser.Current.Parse(s);
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
        private static Func<INormalizerProvider> GetNormalizerProvider = () => NormalizerProvider.Current;
        private string creditCard;
        private const string PARSE_INVALID_EMAIL_EXCEPTION_TEXT = "Invalid email address";
    }
}
