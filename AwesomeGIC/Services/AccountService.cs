using AwesomeGIC.DTO;

namespace AwesomeGIC.Services
{
    public class AccountService : IAccountService
    {
        private readonly Dictionary<string, Account> accounts = new();

        public void AddTransaction(DateTime date, string accountName, string type, decimal amount)
        {
            if (!accounts.ContainsKey(accountName))
                accounts[accountName] = new Account(accountName);

            accounts[accountName].AddTransaction(date, type, amount);
        }

        public IEnumerable<DTO.Transaction> GetTransactions(string accountName)
        {
            if (!accounts.ContainsKey(accountName))
                throw new InvalidOperationException("Account does not exist.");
            return accounts[accountName].Transactions;
        }

        public void PrintAccountStatement(string accountName, int year, int month, List<InterestRule> rules)
        {
            if (!accounts.ContainsKey(accountName))
            {
                Console.WriteLine("Account not found.");
                return;
            }

            accounts[accountName].PrintMonthlyStatement(year, month, rules);
        }
    }
}
