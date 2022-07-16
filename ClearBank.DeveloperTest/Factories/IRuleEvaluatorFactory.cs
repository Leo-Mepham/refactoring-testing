using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.RuleEvaluators;

namespace ClearBank.DeveloperTest.Factories
{
    public interface IRuleEvaluatorFactory
    {
        IRuleEvaluator Get(PaymentScheme paymentScheme);
    }
}
