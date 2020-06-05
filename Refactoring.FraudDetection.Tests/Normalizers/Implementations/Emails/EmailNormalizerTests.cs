// <copyright file="FraudRadarTests.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Normalizers.Common;
using Refactoring.FraudDetection.Normalizers.Implementations.Emails;

namespace Refactoring.FraudDetection.Tests.Normalizers.Implementations.Emails
{
    [TestClass]
    public class EmailNormalizerTests
    {
        [TestMethod]
        public void Normalize_ShouldRemoveDotsInHeadPart()
        {
            var normalizer = GetNormalizer();
            var inputEmail = "a.aa@a.com";
            var expectedEmail = "aaa@a.com";

            var result = normalizer.Normalize(inputEmail);
            result.Should().Be(expectedEmail);
        }

        [TestMethod]
        public void Normalize_ShouldRemoveAllAfterPlusInHeaderPart()
        {
            var normalizer = GetNormalizer();
            var inputEmail = "aaa+bbb@a.com";
            var expectedEmail = "aaa@a.com";

            var result = normalizer.Normalize(inputEmail);
            result.Should().Be(expectedEmail);
        }

        private static IEmailNormalizer GetNormalizer()
        {
            return new EmailNormalizer();
        }
    }
}