using AwesomeGIC.DTO;

namespace AwesomeGIC.Services
{
    public interface IAccountService
    {
        void AddTransaction(DateTime date, string accountName, string type, decimal amount);
        IEnumerable<DTO.Transaction> GetTransactions(string accountName);
        void PrintAccountStatement(string accountName, int year, int month, List<InterestRule> rules);
    }
}
