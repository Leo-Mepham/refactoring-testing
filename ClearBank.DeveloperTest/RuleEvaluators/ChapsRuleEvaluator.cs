using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.RuleEvaluators
{
    public class ChapsRuleEvaluator : IRuleEvaluator
    {
        public bool Evaluate(MakePaymentRequest request, Account account)
        {
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
                return false;

            if (account.Status != AccountStatus.Live)
                return false;

            return true;
        }
    }
}
