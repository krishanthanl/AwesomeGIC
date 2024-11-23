using AwesomeGIC.Services;

namespace AwesomeGIC.Test
{
    [TestClass]
    public class AccountIntegrationTests
    {
        private IAccountService accountService;
        private IInterestRuleService interestRuleService;

        [TestInitialize]
        public void TestInitialize()
        {
            accountService = new AccountService();
            interestRuleService = new InterestRuleService();
        }

        [TestMethod]
        public void PrintMonthlyStatement_WithInterestCalculation_ShouldCalculateCorrectly()
        {
            // Arrange
            var accountName = "AC001";
            interestRuleService.AddInterestRule(new DateTime(2023, 5, 20), "RULE02", 1.90m);
            interestRuleService.AddInterestRule(new DateTime(2023, 6, 15), "RULE03", 2.20m);

            accountService.AddTransaction(new DateTime(2023, 6, 1), accountName, "D", 250.00m);
            accountService.AddTransaction(new DateTime(2023, 6, 26), accountName, "W", 20.00m);
            accountService.AddTransaction(new DateTime(2023, 6, 26), accountName, "W", 100.00m);

            // Act
            accountService.PrintAccountStatement(accountName, 2023, 6, interestRuleService.GetInterestRules());

            // Assert - Validate output manually
        }
    }
}
