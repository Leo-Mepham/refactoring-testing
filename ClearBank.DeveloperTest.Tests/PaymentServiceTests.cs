using System.Collections.Generic;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        [Theory]
        [MemberData(nameof(GetRequestsAndAccounts))]
        public void PaymentServiceEvaluatesRules(MakePaymentRequest request, Account account)
        {
            // Arrange
            var accountDataStore = new Mock<IAccountDataStore>();
            accountDataStore.Setup(a => a.GetAccount(It.IsAny<string>())).Returns(account);

            var accountDataStoreFactory = new Mock<IAccountDataStoreFactory>();
            accountDataStoreFactory.Setup(ad => ad.Get(DataStoreType.Account)).Returns(accountDataStore.Object);

            var configurationService = new Mock<IConfigurationService>();
            configurationService.Setup(c => c.GetDataStoreType()).Returns(DataStoreType.Account);

            var paymentService = new PaymentService(accountDataStoreFactory.Object, configurationService.Object);

            // Act
            var result = paymentService.MakePayment(request);

            // Assert
            result.Success.Should().BeFalse();
        }

        public static IEnumerable<object[]> GetRequestsAndAccounts()
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
    }
}
