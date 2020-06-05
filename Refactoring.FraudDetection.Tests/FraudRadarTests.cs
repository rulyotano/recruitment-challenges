// <copyright file="FraudRadarTests.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Models;

namespace Refactoring.FraudDetection.Tests
{
    [TestClass]
    public class FraudRadarTests
    {
        [TestMethod]
        public void CheckFraud_OneLineFile_NoFraudExpected()
        {
            var result = ExecuteTest(TestCases.OnleLine);

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(0, "The result should not contains fraudulent lines");
        }

        [TestMethod]
        public void CheckFraud_TwoLines_SecondLineFraudulent()
        {
            var result = ExecuteTest(TestCases.TwoLinesFraudulentSecond);

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(1, "The result should contains the number of lines of the file");
            result.First().IsFraudulent.Should().BeTrue("The first line is not fraudulent");
            result.First().OrderId.Should().Be(2, "The first line is not fraudulent");
        }

        [TestMethod]
        public void CheckFraud_ThreeLines_SecondLineFraudulent()
        {
            var result = ExecuteTest(TestCases.ThreeLinesFraudulentSecond);

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(1, "The result should contains the number of lines of the file");
            result.First().IsFraudulent.Should().BeTrue("The first line is not fraudulent");
            result.First().OrderId.Should().Be(2, "The first line is not fraudulent");
        }

        [TestMethod]
        public void CheckFraud_FourLines_MoreThanOneFraudulent()
        {
            var result = ExecuteTest(TestCases.FourLinesMoreTanOneFraud);

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(2, "The result should contains the number of lines of the file");
        }

        [TestMethod]
        public void FraudulentSecond_TwoLines_ByDiffEmails_SameAddress_DiffCards()
        {
            var result = ExecuteTest(TestCases.TwoLinesFraudulentSecondByDiffEmailsSameAddressDiffCards);

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(1, "The result should contains the number of lines of the file");
        }

        [TestMethod]
        public void FraudulentSecond_TwoLines_SameEmailsAfterNormalize()
        {
            var result = ExecuteTest(TestCases.TwoLinesFraudulentSecondSameEmailsAfterNormalized);

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(1, "The result should contains the number of lines of the file");
        }

        [TestMethod]
        public void CheckFraud_SixLines_SameAddressAfterNormalize()
        {
            var result = ExecuteTest(TestCases.SixLinesFraudulentMultiplesSameAddress);
            Func<int, string> kLineIsFraudulent = k => $"The line number ${k} is not fraudulent";

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(3, "The result should have 3 frauds detected");
            result.First().IsFraudulent.Should().BeTrue(kLineIsFraudulent(2));
            result.First().OrderId.Should().Be(2, kLineIsFraudulent(2));
            result.Skip(1).First().IsFraudulent.Should().BeTrue(kLineIsFraudulent(4));
            result.Skip(1).First().OrderId.Should().Be(4, kLineIsFraudulent(4));
            result.Skip(2).First().IsFraudulent.Should().BeTrue(kLineIsFraudulent(6));
            result.Skip(2).First().OrderId.Should().Be(6, kLineIsFraudulent(6));
        }

        private static List<FraudResult> ExecuteTest(IEnumerable<Order> orders)
        {
            var fraudRadar = new FraudRadar();

            return fraudRadar.Check(orders.ToDictionary(it => it.OrderId)).ToList();
        }
    }
}