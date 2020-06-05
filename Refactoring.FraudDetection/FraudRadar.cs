// <copyright file="FraudRadar.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using Refactoring.FraudDetection.Models;
using System.Collections.Generic;
using System.Linq;

namespace Refactoring.FraudDetection
{
    public class FraudRadar
    {
        public IEnumerable<FraudResult> Check(IDictionary<int, Order> ordersInput)
        {
            var fraudResults = new List<FraudResult>();

            var orders = ordersInput.Values.ToArray();

            for (int analizingIndex = 0; analizingIndex < orders.Length; analizingIndex++)
            {
                var currentOrder = orders[analizingIndex];

                for (int comparteToIndex = analizingIndex + 1; comparteToIndex < orders.Length; comparteToIndex++)
                {
                    var compareToOrder = orders[comparteToIndex];

                    if (AreFraudulents(currentOrder, compareToOrder))
                    {
                        fraudResults.Add(new FraudResult(compareToOrder.OrderId, true));
                    }
                }
            }

            return fraudResults;
        }

        private bool AreFraudulents(Order order1, Order order2)
        {
            return (order1.DealId == order2.DealId
                && (order1.Email == order2.Email || order1.Address.Equals(order2.Address))
                && order1.CreditCard != order2.CreditCard);
        }
    }
}