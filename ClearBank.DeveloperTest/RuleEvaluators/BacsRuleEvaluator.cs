using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.RuleEvaluators
{
    public class BacsRuleEvaluator : IRuleEvaluator
    {
        public bool Evaluate(MakePaymentRequest request, Account account)
        {
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                return false;

            return true;
        }
    }
}
