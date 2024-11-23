using AwesomeGIC.DTO;

namespace AwesomeGIC.Services
{
    public class InterestRuleService: IInterestRuleService
    {
        private readonly List<InterestRule> interestRules = new();

        public void AddInterestRule(DateTime date, string ruleId, decimal rate)
        {
            var rule = new InterestRule(date, ruleId, rate);
            interestRules.RemoveAll(r => r.Date == date);
            interestRules.Add(rule);
            interestRules.Sort((a, b) => a.Date.CompareTo(b.Date));
        }

        public List<InterestRule> GetInterestRules() => interestRules;

        public void PrintInterestRules()
        {
            Console.WriteLine("Interest rules:");
            Console.WriteLine("| Date     | RuleId | Rate (%) |");
            foreach (var rule in interestRules)
            {
                Console.WriteLine($"| {rule.Date:yyyyMMdd} | {rule.RuleId,-6} | {rule.Rate,8:F2} |");
            }
        }
    }
}
