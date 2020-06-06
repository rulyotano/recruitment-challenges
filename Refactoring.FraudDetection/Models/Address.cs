using Refactoring.FraudDetection.Normalizers;
using Refactoring.FraudDetection.Normalizers.Common;
using System;

namespace Refactoring.FraudDetection.Models
{
    public class Address : IEquatable<Address>
    {
        public string Street
        {
            get => street;
            set
            {
                var normalizerProvider = GetNormalizerProvider();
                if (normalizerProvider != null)
                {
                    street = normalizerProvider
                        .GetNormalizers(it => it is ICommonNormalizer || it is IStreetNormalizer)
                        .NormalizeAll(value);
                }
                else
                {
                    street = value;
                }
            }
        }

        public string City
        {
            get => city;
            set
            {
                var normalizerProvider = GetNormalizerProvider();
                if (normalizerProvider != null)
                {
                    city = normalizerProvider
                        .GetNormalizers(it => it is ICommonNormalizer || it is ICitytNormalizer)
                        .NormalizeAll(value);
                }
                else
                {
                    city = value;
                }
            }
        }

        public string State
        {
            get => state;
            set
            {
                var normalizerProvider = GetNormalizerProvider();
                if (normalizerProvider != null)
                {
                    state = normalizerProvider
                        .GetNormalizers(it => it is ICommonNormalizer || it is IStateNormalizer)
                        .NormalizeAll(value);
                }
                else
                {
                    state = value;
                }
            }
        }

        public string ZipCode
        {
            get => zipCode;
            set
            {
                var normalizerProvider = GetNormalizerProvider();
                if (normalizerProvider != null)
                {
                    zipCode = normalizerProvider
                        .GetNormalizers(it => it is ICommonNormalizer)
                        .NormalizeAll(value);
                }
                else
                {
                    zipCode = value;
                }
            }
        }

        public bool Equals(Address other)
        {
            if (Street != other.Street)
                return false;
            if (City != other.City)
                return false;
            if (State != other.State)
                return false;
            if (ZipCode != other.ZipCode)
                return false;
            return true;
        }

        public Address(string street, string city,
            string state, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        private static Func<INormalizerProvider> GetNormalizerProvider = () => NormalizerProvider.Current;
        private string state;
        private string street;
        private string city;
        private string zipCode;
    }
}
