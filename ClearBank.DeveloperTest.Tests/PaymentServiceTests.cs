using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Moq;
using Xunit;
using ClearBank.DeveloperTest.RuleEvaluators;

namespace ClearBank.DeveloperTest.Tests
{
    public partial class PaymentServiceTests
    {
        private readonly Mock<IConfigurationService> _configurationService;
        private readonly Mock<IRuleEvaluatorFactory> _ruleEvaluatorFactory;

        public PaymentServiceTests()
        {
            _configurationService = new Mock<IConfigurationService>();
            _configurationService.Setup(c => c.GetDataStoreType()).Returns(DataStoreType.Account);

            _ruleEvaluatorFactory = new Mock<IRuleEvaluatorFactory>();
            _ruleEvaluatorFactory.Setup(p => p.Get(PaymentScheme.Bacs)).Returns(new BacsRuleEvaluator());
            _ruleEvaluatorFactory.Setup(p => p.Get(PaymentScheme.Chaps)).Returns(new ChapsRuleEvaluator());
            _ruleEvaluatorFactory.Setup(p => p.Get(PaymentScheme.FasterPayments)).Returns(new FasterPaymentsRuleEvaluator());
        }

        [Theory]
        [MemberData(nameof(GetFailingRequestsAndAccounts))]
        public void PaymentServiceReturnsFailure(MakePaymentRequest request, Account account)
        {
            // Arrange
            var dataStore = new Mock<IDataStore>();
            dataStore.Setup(a => a.GetAccount(It.IsAny<string>())).Returns(account);

            var dataStoreFactory = new Mock<IDataStoreFactory>();
            dataStoreFactory.Setup(ad => ad.Get(DataStoreType.Account)).Returns(dataStore.Object);

            var paymentService = new PaymentService(
                dataStoreFactory.Object, 
                _configurationService.Object, 
                _ruleEvaluatorFactory.Object);

            // Act
            var result = paymentService.MakePayment(request);

            // Assert
            result.Success.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(GetSucceedingRequestsAndAccounts))]
        public void PaymentServiceReturnsSuccess(MakePaymentRequest request, Account account)
        {
            // Arrange
            var dataStore = new Mock<IDataStore>();
            dataStore.Setup(a => a.GetAccount(It.IsAny<string>())).Returns(account);

            var dataStoreFactory = new Mock<IDataStoreFactory>();
            dataStoreFactory.Setup(ad => ad.Get(DataStoreType.Account)).Returns(dataStore.Object);

            var paymentService = new PaymentService(
                dataStoreFactory.Object,
                _configurationService.Object,
                _ruleEvaluatorFactory.Object);

            // Act
            var result = paymentService.MakePayment(request);

            // Assert
            result.Success.Should().BeTrue();
        }
    }
}
