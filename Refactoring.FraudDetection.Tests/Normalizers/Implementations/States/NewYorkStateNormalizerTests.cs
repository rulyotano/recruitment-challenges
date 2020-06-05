// <copyright file="FraudRadarTests.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Normalizers.Implementations.States;

namespace Refactoring.FraudDetection.Tests.Normalizers.Implementations.States
{
    [TestClass]
    public class NewYorkStateNormalizerTests
    {
        [TestMethod]
        public void Constructor_ShouldCallBaseWithCorrectValues()
        {
            var normalizer = new NewYorkStateNormalizer();

            normalizer.CompleteName.Should().Be(NewYorkStateNormalizer.NEW_YORK_LONG);
            normalizer.ShortName.Should().Be(NewYorkStateNormalizer.NEW_YORK_SHORT);
        }
    }
}