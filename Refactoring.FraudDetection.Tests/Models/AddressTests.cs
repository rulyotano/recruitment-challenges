using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refactoring.FraudDetection.Models.Addresses;
using Refactoring.FraudDetection.Normalizers;
using Refactoring.FraudDetection.Normalizers.Common;
using Refactoring.FraudDetection.Tests.Normalizers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Refactoring.FraudDetection.Tests.Models
{
    [TestClass]
    public class AddressTests
    {
        [TestInitialize]
        public void InitializeTests()
        {
            NormalizerProvider.Current = null;
        }

        [TestCleanup]
        public void CleanTests()
        {
            NormalizerProvider.Current = new DefaultNormalizerProvider();
        }

        #region Constructors
        [TestMethod]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            var address = BuildAddress();

            Assert.AreEqual(
                expected: FAKE_STREET,
                actual: address.Street,
                message: "Street is not correctly setted");

            Assert.AreEqual(
                expected: FAKE_CITY,
                actual: address.City,
                message: "City is not correctly setted");

            Assert.AreEqual(
                expected: FAKE_STATE,
                actual: address.State,
                message: "State is not correctly setted");

            Assert.AreEqual(
                expected: FAKE_ZIPCODE,
                actual: address.ZipCode,
                message: "Zip Code is not correctly setted");
        }
        #endregion

        #region Equals

        [TestMethod]
        public void Equals_StreetChanged_ShouldReturnFalse()
        {
            var address1 = BuildAddress();
            var address2 = BuildAddress(street: "different-street");

            var result = address1.Equals(address2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_CityChanged_ShouldReturnFalse()
        {
            var address1 = BuildAddress();
            var address2 = BuildAddress(city: "different-city");

            var result = address1.Equals(address2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_StateChanged_ShouldReturnFalse()
        {
            var address1 = BuildAddress();
            var address2 = BuildAddress(state: "different-state");

            var result = address1.Equals(address2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_ZipcodeChanged_ShouldReturnFalse()
        {
            var address1 = BuildAddress();
            var address2 = BuildAddress(zipCode: "different-zipcode");

            var result = address1.Equals(address2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_WhenAllTheSame_ShouldReturnTrue()
        {
            var address1 = BuildAddress();
            var address2 = BuildAddress();

            var result = address1.Equals(address2);
            Assert.IsTrue(result);
        }

        #endregion

        #region Street Set

        [TestMethod]
        public void SetStreet_WithNoNormalizer_ShouldSetValue()
        {
            var address = BuildAddress();

            address.Street = CUSTOM_VALUE;

            address.Street.Should().Be(CUSTOM_VALUE);
        }

        [TestMethod]
        public void SetStreet_WithNormalizer_ShouldSetValueAfterNormalize_WithCommonAndStreet()
        {
            var address = BuildAddress();
            NormalizerProvider.Current = GetNormalizerProvider();

            address.Street = CUSTOM_VALUE;

            address.Street.Should().Contain(CUSTOM_VALUE)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND1)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND2)
                .And.Contain(NormalizerTestHelpers.STREET_NORMALIZER_APPEND1)
                .And.Contain(NormalizerTestHelpers.STREET_NORMALIZER_APPEND2)
                .And.NotContain(NormalizerTestHelpers.STATE_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.STATE_NORMALIZER_APPEND2);
        }

        #endregion

        #region State Set

        [TestMethod]
        public void SetState_WithNoNormalizer_ShouldSetValue()
        {
            var address = BuildAddress();

            address.State = CUSTOM_VALUE;

            address.State.Should().Be(CUSTOM_VALUE);
        }

        [TestMethod]
        public void SetState_WithNormalizer_ShouldSetValueAfterNormalize_WithCommonAndStreet()
        {
            var address = BuildAddress();
            NormalizerProvider.Current = GetNormalizerProvider();

            address.State = CUSTOM_VALUE;

            address.State.Should().Contain(CUSTOM_VALUE)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND1)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND2)
                .And.Contain(NormalizerTestHelpers.STATE_NORMALIZER_APPEND1)
                .And.Contain(NormalizerTestHelpers.STATE_NORMALIZER_APPEND2)
                .And.NotContain(NormalizerTestHelpers.STREET_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.STREET_NORMALIZER_APPEND2);
        }

        #endregion

        #region City Set

        [TestMethod]
        public void SetCity_WithNoNormalizer_ShouldSetValue()
        {
            var address = BuildAddress();

            address.City = CUSTOM_VALUE;

            address.City.Should().Be(CUSTOM_VALUE);
        }

        [TestMethod]
        public void SetCity_WithNormalizer_ShouldSetValueAfterNormalize_WithCommonAndStreet()
        {
            var address = BuildAddress();
            NormalizerProvider.Current = GetNormalizerProvider();

            address.City = CUSTOM_VALUE;

            address.City.Should().Contain(CUSTOM_VALUE)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND1)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND2)
                .And.Contain(NormalizerTestHelpers.CITY_NORMALIZER_APPEND1)
                .And.Contain(NormalizerTestHelpers.CITY_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.STREET_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.STREET_NORMALIZER_APPEND2)
                .And.NotContain(NormalizerTestHelpers.STATE_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.STATE_NORMALIZER_APPEND2);
        }

        #endregion

        #region Zip Code Set

        [TestMethod]
        public void SetZipCode_WithNoNormalizer_ShouldSetValue()
        {
            var address = BuildAddress();

            address.ZipCode = CUSTOM_VALUE;

            address.ZipCode.Should().Be(CUSTOM_VALUE);
        }

        [TestMethod]
        public void SetZipCode_WithNormalizer_ShouldSetValueAfterNormalize_WithCommonAndStreet()
        {
            var address = BuildAddress();
            NormalizerProvider.Current = GetNormalizerProvider();

            address.ZipCode = CUSTOM_VALUE;

            address.ZipCode.Should().Contain(CUSTOM_VALUE)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND1)
                .And.Contain(NormalizerTestHelpers.COMMON_NORMALIZER_APPEND2)
                .And.NotContain(NormalizerTestHelpers.CITY_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.CITY_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.STREET_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.STREET_NORMALIZER_APPEND2)
                .And.NotContain(NormalizerTestHelpers.STATE_NORMALIZER_APPEND1)
                .And.NotContain(NormalizerTestHelpers.STATE_NORMALIZER_APPEND2);
        }

        #endregion

        private static Address BuildAddress(string street = FAKE_STREET, string city = FAKE_CITY,
            string state = FAKE_STATE, string zipCode = FAKE_ZIPCODE)
        {
            return new Address(street, city, state, zipCode);
        }

        private static INormalizerProvider GetNormalizerProvider()
        {
            return new ParameterizedNormalizerProvider(NormalizerTestHelpers.GetFakeNormalizers());
        }

        private const string FAKE_STREET = "fake-street";
        private const string FAKE_CITY = "fake-city";
        private const string FAKE_STATE = "fake-state";
        private const string FAKE_ZIPCODE = "fake-zip-code";
        private const string CUSTOM_VALUE = "custom-value";
    }
}
