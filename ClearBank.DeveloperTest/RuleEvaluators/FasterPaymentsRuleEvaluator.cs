using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.RuleEvaluators
{
    public class FasterPaymentsRuleEvaluator : IRuleEvaluator
    {
        public bool Evaluate(MakePaymentRequest request, Account account)
        {
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
                return false;

            if (account.Balance < request.Amount)
                return false;

            return true;
        }
    }
}
