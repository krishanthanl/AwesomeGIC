
using AwesomeGIC.Services;
using AwesomeGIC.Utils;
using System.Globalization;
public class Program
{
    private static readonly IAccountService accountService = new AccountService();
    private static readonly IInterestRuleService interestRuleService = new InterestRuleService();
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Welcome to AwesomeGIC Bank! What would you like to do?");
            Console.WriteLine("[T] Input transactions");
            Console.WriteLine("[I] Define interest rules");
            Console.WriteLine("[P] Print statement");
            Console.WriteLine("[Q] Quit");
            Console.Write(">");
            string? choice = Console.ReadLine()?.Trim().ToUpper();

            switch (choice)
            {
                case BankActivityType.InputTransactions:
                    HandleTransaction();
                    break;
                case BankActivityType.DefineInterestRules:
                    HandleInterestRules();
                    break;
                case BankActivityType.PrintStatement:
                    HandlePrintStatement();
                    break;
                case BankActivityType.Quit:
                    Console.WriteLine("Thank you for banking with AwesomeGIC Bank.");
                    Console.WriteLine("Have a nice day!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void PrintAccountStatement(string account)
    {
        Console.WriteLine($"Account: {account}");
        Console.WriteLine("| Date     | Txn Id      | Type | Amount | Balance |");
        foreach (var txn in accountService.GetTransactions(account))
        {
            Console.WriteLine($"| {txn.Date:yyyyMMdd} | {txn.Id,-10} | {txn.Type,-4} | {txn.Amount,7:F2} | {txn.Balance,8:F2} |");
        }
    }

    static void HandleTransaction()
    {
        while (true)
        {
            Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format");
            Console.WriteLine("(or enter blank to go back to main menu):");
            Console.Write(">");

            string? input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input))
            {
                break;
            }
            var parts = input.Split(' ');
            if (
                parts.Length != 4 ||
                !DateTime.TryParseExact(parts[0], "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) ||
                (parts[2].ToUpper() != TransactionType.Diposit && parts[2].ToUpper() != TransactionType.Withdrawal) ||
                !decimal.TryParse(parts[3], out var amount) || amount <= 0
                )
            {
                Console.WriteLine("Invalid Input Please Note Date:yyyyMMdd\nTransaction type (Only Allow D - Diposit, W - Withdrawal). \nAmount > 0");
                continue;
            }

            var account = parts[1];
            var type = parts[2].ToUpper();

            try
            {
                accountService.AddTransaction(date, account, type, amount);
                PrintAccountStatement(account);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void HandleInterestRules()
    {
        while (true)
        {
            Console.WriteLine("Please enter interest rules details in <Date> <RuleId> <Rate in %> format");
            Console.WriteLine("(or enter blank to go back to main menu):");
            Console.Write(">");
            string? input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) break;

            var parts = input.Split(' ');
            if (parts.Length != 3 ||
                !DateTime.TryParseExact(parts[0], "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) ||
                !decimal.TryParse(parts[2], out var rate) || rate <= 0 || rate >= 100)
            {
                Console.WriteLine("Invalid input. Please follows above mentioned format");
                continue;
            }

            var ruleId = parts[1];
            interestRuleService.AddInterestRule(date, ruleId, rate);
            interestRuleService.PrintInterestRules();
        }
    }

    static void HandlePrintStatement()
    {
        while (true)
        {
            Console.WriteLine("Please enter account and month to generate the statement <Account> <Year><Month>");
            Console.WriteLine("(or enter blank to go back to main menu):");
            Console.Write(">");
            string? input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) break;

            var parts = input.Split(' ');
            if (parts.Length != 2 || !DateTime.TryParseExact(parts[1], "yyyyMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                Console.WriteLine("Invalid input. Please follow the above mentioned format");
                continue;
            }

            var account = parts[0];
            accountService.PrintAccountStatement(account, date.Year, date.Month, interestRuleService.GetInterestRules());
        }
    }
}

