using AwesomeGIC.Services;

namespace AwesomeGIC.Test
{
    [TestClass]
    public class AccountServiceTests
    {

        private IAccountService accountService;

        [TestInitialize]
        public void TestInitialize()
        {
            accountService = new AccountService();
        }

        [TestMethod]
        public void AddTransaction_Deposit_ShouldIncreaseBalance()
        {
            // Arrange
            var accountName = "AC001";
            var date = new DateTime(2023, 6, 1);

            // Act
            accountService.AddTransaction(date, accountName, "D", 150.00m);

            // Assert
            var transactions = accountService.GetTransactions(accountName).ToList();
            Assert.AreEqual(1, transactions.Count);
            Assert.AreEqual(150.00m, transactions[0].Balance);
        }

        [TestMethod]
        public void AddTransaction_Withdrawal_ShouldDecreaseBalance()
        {
            // Arrange
            var accountName = "AC001";
            var date = new DateTime(2023, 6, 1);
            accountService.AddTransaction(date, accountName, "D", 200.00m);

            // Act
            accountService.AddTransaction(date, accountName, "W", 50.00m);

            // Assert
            var transactions = accountService.GetTransactions(accountName).ToList();
            Assert.AreEqual(2, transactions.Count);
            Assert.AreEqual(150.00m, transactions[^1].Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddTransaction_WithdrawalInsufficientFunds_ShouldThrowException()
        {
            // Arrange
            var accountName = "AC001";
            var date = new DateTime(2023, 6, 1);
            accountService.AddTransaction(date, accountName, "D", 100.00m);

            // Act
            accountService.AddTransaction(date, accountName, "W", 150.00m);

            // Assert - Exception is expected
        }

        [TestMethod]
        public void GetTransactions_NonexistentAccount_ShouldReturnEmptyList()
        {
            Assert.ThrowsException<InvalidOperationException>(() => accountService.GetTransactions("blabla"));
        }
    }
}