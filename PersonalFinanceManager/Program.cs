using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Transaction
{
    public string Type { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Notes { get; set; }

    public Transaction(string type, string category, decimal amount, DateTime date, string notes)
    {
        Type = type;
        Category = category;
        Amount = amount;
        Date = date;
        Notes = notes;
    }
}

public class Budget
{
    public decimal MonthlyBudget { get; set; }
    public Dictionary<string, decimal> CategoryBudgets { get; set; }

    public Budget(decimal monthlyBudget)
    {
        MonthlyBudget = monthlyBudget;
        CategoryBudgets = new Dictionary<string, decimal>();
    }

    public void SetCategoryBudget(string category, decimal amount)
    {
        CategoryBudgets[category] = amount;
    }
}

public class PersonalFinanceManager
{
    private List<Transaction> transactions = new List<Transaction>();
    private Budget budget;

    private const string DataFilePath = "finance_data.json";

    public PersonalFinanceManager(decimal monthlyBudget)
    {
        budget = new Budget(monthlyBudget);
        LoadData();
    }

    public void AddIncome(decimal amount, DateTime date, string source)
    {
        transactions.Add(new Transaction("Income", "General", amount, date, source));
        Console.WriteLine("Income added successfully.");
    }

    public void AddExpense(decimal amount, string category, DateTime date, string notes)
    {
        transactions.Add(new Transaction("Expense", category, amount, date, notes));
        Console.WriteLine("Expense logged successfully.");
    }

    public void DisplayMonthlySummary()
    {
        decimal totalIncome = transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
        decimal totalExpenses = transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);
        decimal balance = totalIncome - totalExpenses;
        Console.WriteLine($"\nMonthly Summary:\nTotal Income: ${totalIncome}\nTotal Expenses: ${totalExpenses}\nBalance: ${balance}");
    }

    public void DisplayCategoryBreakdown()
    {
        Console.WriteLine("\nExpense Breakdown by Category:");
        var categoryTotals = transactions
            .Where(t => t.Type == "Expense")
            .GroupBy(t => t.Category)
            .Select(g => new { Category = g.Key, Total = g.Sum(t => t.Amount) });

        foreach (var category in categoryTotals)
        {
            Console.WriteLine($"{category.Category}: ${category.Total}");
            if (budget.CategoryBudgets.ContainsKey(category.Category) && category.Total > budget.CategoryBudgets[category.Category])
            {
                Console.WriteLine($"- Alert: Over budget in {category.Category}!");
            }
        }
    }

    public void SetCategoryBudget(string category, decimal amount)
    {
        budget.SetCategoryBudget(category, amount);
        Console.WriteLine("Category budget set successfully.");
    }

    public void SaveData()
    {
        var data = new
        {
            Transactions = transactions,
            Budget = budget
        };

        string jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(DataFilePath, jsonData);
        Console.WriteLine("Data saved successfully.");
    }

    private void LoadData()
    {
        if (File.Exists(DataFilePath))
        {
            string jsonData = File.ReadAllText(DataFilePath);
            var data = JsonSerializer.Deserialize<FinanceData>(jsonData);

            if (data != null)
            {
                transactions = data.Transactions ?? new List<Transaction>();
                budget = data.Budget ?? new Budget(budget.MonthlyBudget);
                Console.WriteLine("Data loaded successfully.");
            }
        }
        else
        {
            Console.WriteLine("No previous data found. Starting fresh.");
        }
    }

    private class FinanceData
    {
        public List<Transaction> Transactions { get; set; }
        public Budget Budget { get; set; }
    }
}

public class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Set your monthly budget: ");
        decimal monthlyBudget = decimal.Parse(Console.ReadLine());
        PersonalFinanceManager financeManager = new PersonalFinanceManager(monthlyBudget);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nPersonal Finance Manager");
            Console.WriteLine("1. Add Income");
            Console.WriteLine("2. Add Expense");
            Console.WriteLine("3. Set Category Budget");
            Console.WriteLine("4. View Monthly Summary");
            Console.WriteLine("5. View Expense Breakdown by Category");
            Console.WriteLine("6. Save and Exit");

            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter income amount: ");
                    decimal incomeAmount = decimal.Parse(Console.ReadLine());
                    Console.Write("Enter income date (yyyy-mm-dd): ");
                    DateTime incomeDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Enter income source: ");
                    string source = Console.ReadLine();
                    financeManager.AddIncome(incomeAmount, incomeDate, source);
                    break;

                case "2":
                    Console.Write("Enter expense amount: ");
                    decimal expenseAmount = decimal.Parse(Console.ReadLine());
                    Console.Write("Enter expense category: ");
                    string category = Console.ReadLine();
                    Console.Write("Enter expense date (yyyy-mm-dd): ");
                    DateTime expenseDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Enter notes: ");
                    string notes = Console.ReadLine();
                    financeManager.AddExpense(expenseAmount, category, expenseDate, notes);
                    break;

                case "3":
                    Console.Write("Enter category name: ");
                    string cat = Console.ReadLine();
                    Console.Write("Set budget for this category: ");
                    decimal catBudget = decimal.Parse(Console.ReadLine());
                    financeManager.SetCategoryBudget(cat, catBudget);
                    break;

                case "4":
                    financeManager.DisplayMonthlySummary();
                    break;

                case "5":
                    financeManager.DisplayCategoryBreakdown();
                    break;

                case "6":
                    financeManager.SaveData();
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid option Try again........................");
                    break;
            }
        }
    }
}