// <copyright file="FraudRadarTests.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Normalizers.Implementations.States;

namespace Refactoring.FraudDetection.Tests.Normalizers.Implementations.States
{
    [TestClass]
    public class CaliforniaStateNormalizerTests
    {
        [TestMethod]
        public void Constructor_ShouldCallBaseWithCorrectValues()
        {
            var normalizer = new CaliforniaStateNormalizer();

            normalizer.CompleteName.Should().Be(CaliforniaStateNormalizer.CALIFORNIA_LONG);
            normalizer.ShortName.Should().Be(CaliforniaStateNormalizer.CALIFORNIA_SHORT);
        }
    }
}