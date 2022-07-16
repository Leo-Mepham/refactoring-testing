using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.RuleEvaluators
{
    public interface IRuleEvaluator
    {
        bool Evaluate(MakePaymentRequest request, Account account);
    }
}
