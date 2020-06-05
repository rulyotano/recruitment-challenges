// <copyright file="FraudRadarTests.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Normalizers.Implementations.States;

namespace Refactoring.FraudDetection.Tests.Normalizers.Implementations.States
{
    [TestClass]
    public class IllinoiseStateNormalizerTests
    {
        [TestMethod]
        public void Constructor_ShouldCallBaseWithCorrectValues()
        {
            var normalizer = new IllinoisStateNormalizer();

            normalizer.CompleteName.Should().Be(IllinoisStateNormalizer.ILLINOIS_LONG);
            normalizer.ShortName.Should().Be(IllinoisStateNormalizer.ILLINOIS_SHORT);
        }
    }
}