using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDataStoreFactory _dataStoreFactory;
        private readonly IConfigurationService _configurationService;
        private readonly IRuleEvaluatorFactory _ruleEvaluatorFactory;

        public PaymentService(
            IDataStoreFactory dataStoreFactory,
            IConfigurationService configurationService,
            IRuleEvaluatorFactory ruleEvaluatorFactory)
        {
            // Assuming DI is present
            _dataStoreFactory = dataStoreFactory;
            _configurationService = configurationService;
            _ruleEvaluatorFactory = ruleEvaluatorFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var dataStoreType = _configurationService.GetDataStoreType();
            var accountDataStore = _dataStoreFactory.Get(dataStoreType);
            var account = accountDataStore.GetAccount(request.DebtorAccountNumber);

            if (account == null)
                return new MakePaymentResult { Success = false };

            var ruleEvaluator = _ruleEvaluatorFactory.Get(request.PaymentScheme);
            var result = new MakePaymentResult
            {
                Success = ruleEvaluator.Evaluate(request, account)
            };

            if (result.Success)
            {
                account.Balance -= request.Amount;
                accountDataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
