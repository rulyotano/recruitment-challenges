// <copyright file="FraudRadarTests.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Normalizers.Common;
using Refactoring.FraudDetection.Normalizers.Implementations.Emails;

namespace Refactoring.FraudDetection.Tests.Normalizers.Implementations.Common
{
    [TestClass]
    public class TrimNormalizerTests
    {
        [TestMethod]
        public void Normalize_ShouldRemoveSpaceFromStartAndEnd()
        {
            var normalizer = GetNormalizer();
            var input = "   test   ";
            var expected = "test";

            var result = normalizer.Normalize(input);
            result.Should().Be(expected);
        }

        private static ICommonNormalizer GetNormalizer()
        {
            return new TrimNormalizer();
        }
    }
}