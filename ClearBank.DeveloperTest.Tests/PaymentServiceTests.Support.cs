using System.Collections.Generic;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests
{
    public partial class PaymentServiceTests
    {
        public static IEnumerable<object[]> GetFailingRequestsAndAccounts()
        {
            // Null accounts should fail
            yield return new object[]
            {
                new MakePaymentRequest(),
                null
            };

            // Bacs not an allowed payment scheme
            yield return new object[]
            {
                new MakePaymentRequest
                {
                    PaymentScheme = PaymentScheme.Bacs
                },
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments
                }
            };

            // Chaps not an allowed payment scheme
            yield return new object[]
            {
                new MakePaymentRequest
                {
                    PaymentScheme = PaymentScheme.Chaps
                },
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.FasterPayments
                }
            };

            // FasterPayments not an allowed payment scheme
            yield return new object[]
            {
                new MakePaymentRequest
                {
                    PaymentScheme = PaymentScheme.FasterPayments
                },
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps
                }
            };

            // FasterPayments must have sufficient balance
            yield return new object[]
            {
                new MakePaymentRequest
                {
                    PaymentScheme = PaymentScheme.FasterPayments,
                    Amount = 1
                },
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                    Balance = 0
                }
            };

            // Chaps must be a live account (Not disabled)
            yield return new object[]
            {
                new MakePaymentRequest
                {
                    PaymentScheme = PaymentScheme.Chaps
                },
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Status = AccountStatus.Disabled
                }
            };

            // Chaps must be a live account (Not inbound only)
            yield return new object[]
            {
                new MakePaymentRequest
                {
                    PaymentScheme = PaymentScheme.Chaps
                },
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Status = AccountStatus.InboundPaymentsOnly
                }
            };
        }

        public static IEnumerable<object[]> GetSucceedingRequestsAndAccounts()
        {
            // Bacs an allowed payment scheme
            yield return new object[]
            {
                new MakePaymentRequest
                {
                    PaymentScheme = PaymentScheme.Bacs
                },
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
                }
            };

            // Chaps an allowed payment scheme, and account live
            yield return new object[]
            {
                new MakePaymentRequest
                {
                    PaymentScheme = PaymentScheme.Chaps
                },
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Status = AccountStatus.Live
                }
            };

            // FasterPayments an allowed payment scheme, and sufficent balance
            yield return new object[]
            {
                new MakePaymentRequest
                {
                    PaymentScheme = PaymentScheme.FasterPayments,
                    Amount = 1
                },
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                    Balance = 1
                }
            };
        }
    }
}
