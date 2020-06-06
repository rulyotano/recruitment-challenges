using Refactoring.FraudDetection.Models;
using System.Collections.Generic;

namespace Refactoring.FraudDetection.Tests
{
    public static class TestCases
    {
        public static IEnumerable<Order> FourLinesMoreTanOneFraud
        {
            get
            {
                return new Order[]
                {
                    Order.Parse("1,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010"),
                    Order.Parse("2,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689011"),
                    Order.Parse("3,2,roger@rabbit.com,1234 Not Sesame St.,Colorado,CL,10012,12345689012"),
                    Order.Parse("4,2,roger@rabbit.com,1234 Not Sesame St.,Colorado,CL,10012,12345689014"),
                };
            }
        }

        public static IEnumerable<Order> OnleLine
        {
            get
            {
                return new Order[]
                {
                    Order.Parse("1,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010"),
                };
            }
        }

        public static IEnumerable<Order> SixLinesFraudulentMultiplesSameAddress
        {
            get
            {
                return new Order[]
                {
                    Order.Parse("1,1,bugs@bunny.com,123 Sesame street,New York,NY,10011,12345689010"),
                    Order.Parse("2,1,duck@lucas.com,123 sesame street,New York,NY,10011,12345689011"),
                    Order.Parse("3,2,bugs@bunny.com,123 Sesame road,New York,Illinois,10011,12345689010"),
                    Order.Parse("4,2,duck@lucas.com,123 sesame road,New York,Illinois,10011,12345689011"),
                    Order.Parse("5,3,bugs@bunny.com,123 Sesame St.,New York,California,10011,12345689010"),
                    Order.Parse("6,3,duck@lucas.com,123 Sesame St.,New York,California,10011,12345689011"),
                };
            }
        }

        public static IEnumerable<Order> ThreeLinesFraudulentSecond
        {
            get
            {
                return new Order[]
                {
                    Order.Parse("1,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010"),
                    Order.Parse("2,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689011"),
                    Order.Parse("3,2,roger@rabbit.com,1234 Not Sesame St.,Colorado,CL,10012,12345689012"),
                };
            }
        }

        public static IEnumerable<Order> TwoLinesFraudulentSecond
        {
            get
            {
                return new Order[]
                {
                    Order.Parse("1,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010"),
                    Order.Parse("2,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689011"),
                };
            }
        }

        public static IEnumerable<Order> TwoLinesFraudulentSecondByDiffEmailsSameAddressDiffCards
        {
            get
            {
                return new Order[]
                {
                    Order.Parse("1,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010"),
                    Order.Parse("2,1,duck@lucas.com,123 Sesame St.,New York,NY,10011,12345689011"),
                };
            }
        }

        public static IEnumerable<Order> TwoLinesFraudulentSecondSameEmailsAfterNormalized
        {
            get
            {
                return new Order[]
                {
                    Order.Parse("1,1,b.ugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010"),
                    Order.Parse("2,1,bugs+uncle@bunny.com,Any,Any,Any,Any,12345689011"),
                };
            }
        }

        public static IEnumerable<Order> ThreeLinesRepeatedId
        {
            get
            {
                return new Order[]
                {
                    Order.Parse("1,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010"),
                    Order.Parse("2,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689011"),
                    Order.Parse("1,2,roger@rabbit.com,1234 Not Sesame St.,Colorado,CL,10012,12345689012"),
                };
            }
        }
    }
}
