using Refactoring.FraudDetection.Normalizers;
using Refactoring.FraudDetection.Normalizers.Common;
using System;

namespace Refactoring.FraudDetection.Models.Addresses
{
    public class Address : IEquatable<Address>
    {
        public string Street
        {
            get => street; 
            set
            {
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

        public string City { get; set; }

        public string State
        {
            get => state;
            set
            {
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

        public string ZipCode { get; set; }

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

        public static void UseNormalizers(INormalizerProvider normalizerProvider)
        {
            Address.normalizerProvider = normalizerProvider;
        }

        public Address(string street, string city,
            string state, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        private static INormalizerProvider normalizerProvider;
        private string state;
        private string street;
    }
}
