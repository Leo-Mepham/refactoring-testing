using System;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.RuleEvaluators;

namespace ClearBank.DeveloperTest.Factories
{
    public class RuleEvaluatorFactory : IRuleEvaluatorFactory
    {
        public IRuleEvaluator Get(PaymentScheme paymentScheme)
        {
            switch (paymentScheme)
            {
                case PaymentScheme.Bacs: return new BacsRuleEvaluator();
                case PaymentScheme.Chaps: return new ChapsRuleEvaluator();
                case PaymentScheme.FasterPayments: return new FasterPaymentsRuleEvaluator();
                default: throw new ArgumentException($"Unknown PaymentScheme {paymentScheme}");
            }
        }
    }
}
