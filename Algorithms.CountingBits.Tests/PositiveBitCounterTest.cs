// <copyright file="PositiveBitCounterTest.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Algorithms.CountingBits.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PositiveBitCounterTest
    {
        private readonly PositiveBitCounter bitCounter = new PositiveBitCounter();

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Count_NegativeValue_ArgumentExceptionExpected()
        {
            this.bitCounter.Count(-2);
        }

        [TestMethod]
        public void Count_Zero_NoOccurrences()
        {
            CollectionAssert.AreEqual(
                expected: new List<int> { 0 },
                actual: this.bitCounter.Count(0).ToList(),
                message: "The result is not the expected");
        }

        [TestMethod]
        public void Count_ValidInput_OneOcurrence()
        {
            CollectionAssert.AreEqual(
                expected: new List<int> { 1, 0 },
                actual: this.bitCounter.Count(1).ToList(),
                message: "The result is not the expected");
        }

        [TestMethod]
        public void Count_ValidInput_MultipleOcurrence()
        {
            CollectionAssert.AreEqual(
                expected: new List<int> { 3, 0, 5, 7 },
                actual: this.bitCounter.Count(161).ToList(),
                message: "The result is not the expected");
        }

        [DataTestMethod]
        [DataRow(37, "3,0,2,5")]
        [DataRow(33, "2,0,5")]
        [DataRow(100, "3,2,5,6")]
        [DataRow(100, "3,2,5,6")]
        [DataRow(33566, "7,1,2,3,4,8,9,15")]
        [DataRow(5424545, "10,0,5,7,8,10,14,15,17,20,22")]
        public void Count_WithSeveralTestCases_InputShouldReturnExpectedResult(int input, string expectedString)
        {
            Assert.AreEqual(
                expected: expectedString,
                actual: string.Join(',', bitCounter.Count(input)),
                message: "The result is not the expected");
        }
    }
}