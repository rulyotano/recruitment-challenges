// <copyright file="FraudRadarTests.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Normalizers.Common;
using Refactoring.FraudDetection.Normalizers.Implementations.Emails;
using Refactoring.FraudDetection.Normalizers.Implementations.States;

namespace Refactoring.FraudDetection.Tests.Normalizers.Implementations.States
{
    [TestClass]
    public class BaseStateNormalizerTests
    {
        [DataTestMethod]
        [DataRow(SHORT_NAME, LONG_NAME, SHORT_NAME, LONG_NAME, "When input is same as the shortName, should replace all word")]
        [DataRow(SHORT_NAME, LONG_NAME, LONG_NAME, LONG_NAME, "When input is same as the longName, should return same word")]
        [DataRow(SHORT_NAME, LONG_NAME, SHORT_NAME_INSIDE, LONG_NAME_NAME_INSIDE, "When short name is inside, should replace just that word")]
        public void Normalize_TestCases(string shortName, string longName,
            string inputText, string expected, string errorDescription)
        {
            var normalizer = GetFakeStateNormalizer(shortName, longName);

            var result = normalizer.Normalize(inputText);
            result.Should().Be(expected, because: errorDescription);
        }

        private IStateNormalizer GetFakeStateNormalizer(string shortName, string completeName)
        {
            return new FakeStateNormalizer(shortName, completeName);
        }

        private const string SHORT_NAME = "il";
        private const string LONG_NAME = "illinois";
        private const string SHORT_NAME_INSIDE = "state il state";
        private const string LONG_NAME_NAME_INSIDE = "state illinois state";
    }

    public class FakeStateNormalizer : StateNormalizerBase
    {
        public FakeStateNormalizer(string shortName, string completeName)
            : base(shortName, completeName)
        {
        }
    }
}