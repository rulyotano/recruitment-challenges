// <copyright file="FraudRadar.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using Refactoring.FraudDetection.Models;
using Refactoring.FraudDetection.OrderProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Refactoring.FraudDetection
{
    public class FraudRadarFromFile
    {
        public async Task<IEnumerable<FraudResult>> Check(string filePath)
        {
            IOrderProvider parser = new FromFileOrderProvider(filePath);

            var fraudRadar = new FraudRadar();

            return fraudRadar.Check(await parser.GetOrdersAsync());
        }
    }
}