// <copyright file="FraudRadar.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Refactoring.FraudDetection.Models
{
    public class FraudResult
    {
        public int OrderId { get; protected set; }

        public bool IsFraudulent { get; protected set; }

        public FraudResult(int orderId, bool isFraudulent)
        {
            OrderId = orderId;
            IsFraudulent = isFraudulent;
        }
    }
}