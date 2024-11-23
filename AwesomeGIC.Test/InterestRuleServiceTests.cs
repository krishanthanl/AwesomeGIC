using AwesomeGIC.Services;

namespace AwesomeGIC.Test
{
    [TestClass]
    public class InterestRuleServiceTests
    {
        private IInterestRuleService interestRuleService;

        [TestInitialize]
        public void TestInitialize()
        {
            interestRuleService = new InterestRuleService();
        }

        [TestMethod]
        public void AddInterestRule_ShouldStoreRule()
        {
            // Arrange
            var date = new DateTime(2023, 6, 15);

            // Act
            interestRuleService.AddInterestRule(date, "RULE01", 2.20m);

            // Assert
            var rules = interestRuleService.GetInterestRules();
            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("RULE01", rules[0].RuleId);
            Assert.AreEqual(2.20m, rules[0].Rate);
        }

        [TestMethod]
        public void AddInterestRule_SameDate_ShouldReplaceExistingRule()
        {
            // Arrange
            var date = new DateTime(2023, 6, 15);
            interestRuleService.AddInterestRule(date, "RULE01", 2.20m);

            // Act
            interestRuleService.AddInterestRule(date, "RULE02", 2.50m);

            // Assert
            var rules = interestRuleService.GetInterestRules();
            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("RULE02", rules[0].RuleId);
            Assert.AreEqual(2.50m, rules[0].Rate);
        }

        [TestMethod]
        public void GetInterestRules_ShouldReturnSortedRules()
        {
            // Arrange
            interestRuleService.AddInterestRule(new DateTime(2023, 6, 15), "RULE03", 1.95m);
            interestRuleService.AddInterestRule(new DateTime(2023, 5, 20), "RULE02", 1.90m);
            interestRuleService.AddInterestRule(new DateTime(2023, 1, 1), "RULE01", 1.85m);

            // Act
            var rules = interestRuleService.GetInterestRules();

            // Assert
            Assert.AreEqual(3, rules.Count);
            Assert.AreEqual("RULE01", rules[0].RuleId);
            Assert.AreEqual("RULE02", rules[1].RuleId);
            Assert.AreEqual("RULE03", rules[2].RuleId);
        }


    }
}
