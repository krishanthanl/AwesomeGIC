
namespace AwesomeGIC.DTO
{
    public class Account
    {
        public string Name { get; }
        public List<Transaction> Transactions { get; }
        private decimal Balance { get; set; }

        public Account(string name)
        {
            Name = name;
            Transactions = new List<Transaction>();
            Balance = 0;
        }

        public void AddTransaction(DateTime date, string type, decimal amount)
        {
            if (type == "W" && Balance < amount)
                throw new InvalidOperationException("Insufficient funds.");

            Balance += type == "D" ? amount : -amount;
            var id = $"{date:yyyyMMdd}-{Transactions.Count(t => t.Date == date) + 1:D2}";
            Transactions.Add(new Transaction(date, id, type, amount, Balance));
        }

        public void PrintMonthlyStatement(int year, int month, List<InterestRule> rules)
        {
            Console.WriteLine($"Account: {Name}");
            Console.WriteLine("| Date     | Txn Id      | Type | Amount | Balance |");
            foreach (var txn in Transactions.Where(t => t.Date.Year == year && t.Date.Month == month))
            {
                Console.WriteLine($"| {txn.Date:yyyyMMdd} | {txn.Id,-10} | {txn.Type,-4} | {txn.Amount,7:F2} | {txn.Balance,8:F2} |");
            }
            // Interest calculation and printing to be added based on rules
        }
    }
}
