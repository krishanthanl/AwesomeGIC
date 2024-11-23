
namespace AwesomeGIC.DTO
{
    public class Transaction
    {
        public DateTime Date { get; }
        public string Id { get; }
        public string Type { get; }
        public decimal Amount { get; }
        public decimal Balance { get; }

        public Transaction(DateTime date, string id, string type, decimal amount, decimal balance)
        {
            Date = date;
            Id = id;
            Type = type;
            Amount = amount;
            Balance = balance;
        }
    }
}
