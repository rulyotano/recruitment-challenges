// <copyright file="PositiveBitCounter.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Algorithms.CountingBits
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PositiveBitCounter
    {
        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
                throw new ArgumentException("Input can't be negavite.");

            var count = 0;
            var currentIndex = 0;
            var positions = new List<int> { };
            var currentNumber = input;

            while (currentNumber > 0)
            {
                var isOne = currentNumber % 2 == 1;
                if (isOne)
                {
                    count++;
                    positions.Add(currentIndex);
                }
                currentNumber /= 2;
                currentIndex++;
            }

            return new List<int> { count }.Concat(positions);
        }
    }
}
