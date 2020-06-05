// <copyright file="FraudRadarTests.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Normalizers.Common;
using Refactoring.FraudDetection.Normalizers.Implementations.Streets;

namespace Refactoring.FraudDetection.Tests.Normalizers.Implementations.Streets
{
    [TestClass]
    public class StreetNormalizerTests
    {
        [TestMethod]
        public void Normalize_ShouldReplaceStPerStreet()
        {
            var normalizer = GetNormalizer();
            var input = "st.";
            var expected = "street";

            var result = normalizer.Normalize(input);
            result.Should().Be(expected);
        }

        [TestMethod]
        public void Normalize_ShouldReplaceRdPerRoad()
        {
            var normalizer = GetNormalizer();
            var input = "rd.";
            var expected = "road";

            var result = normalizer.Normalize(input);
            result.Should().Be(expected);
        }

        [TestMethod]
        public void Normalize_Road_ShouldNotReplaceWhenFinalizePattaer()
        {
            var normalizer = GetNormalizer();
            var input = "...perd.";
            var expected = "...perd.";

            var result = normalizer.Normalize(input);
            result.Should().Be(expected);
        }

        [TestMethod]
        public void Normalize_Street_ShouldNotReplaceWhenFinalizePattaer()
        {
            var normalizer = GetNormalizer();
            var input = "...first.";
            var expected = "...first.";

            var result = normalizer.Normalize(input);
            result.Should().Be(expected);
        }

        private static IStreetNormalizer GetNormalizer()
        {
            return new StreetNormalizer();
        }
    }
}