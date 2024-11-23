using AwesomeGIC.DTO;

namespace AwesomeGIC.Services
{
    public interface IInterestRuleService
    {
        void AddInterestRule(DateTime date, string ruleId, decimal rate);
        List<InterestRule> GetInterestRules();
        void PrintInterestRules();
    }
}
