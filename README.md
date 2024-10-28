
# Personal Finance Manager

A console-based application written in C# for managing personal finances. This application allows users to log income and expenses, set a monthly budget, allocate budgets for specific categories, and view detailed financial summaries. Data is saved in a JSON file, allowing for data persistence across sessions.

## Features

- **Add Income and Expense**: Log income sources and expenses by category.
- **Category-Based Budgeting**: Set budget limits for specific categories and receive alerts if exceeded.
- **Monthly Summary**: View total income, expenses, and remaining balance.
- **Expense Breakdown**: Get a detailed breakdown of expenses by category.
- **Data Persistence**: All transactions and budget settings are saved to a JSON file for data persistence.

## Project Structure

- **Transaction Class**: Represents individual transactions (income or expense).
- **Budget Class**: Stores the monthly budget and category-specific budgets.
- **PersonalFinanceManager Class**: Main class that manages transactions, budgets, and file operations.
- **Program Class**: Contains the main application loop, displaying a menu and handling user input.

## Getting Started

### Prerequisites

- .NET SDK (6.0 or later)
- Basic knowledge of C#

### Installation

1. Clone this repository:

   ```bash
   git clone https://github.com/yourusername/PersonalFinanceManager.git
   cd PersonalFinanceManager
   ```

2. Open the project in Visual Studio or your preferred C# editor.

3. Build the project:

   ```bash
   dotnet build
   ```

4. Run the application:

   ```bash
   dotnet run
   ```

### Usage

1. **Set Monthly Budget**: Enter your monthly budget at the start.
2. **Main Menu**: 
   - **Add Income**: Enter details for income, including amount, date, and source.
   - **Add Expense**: Log an expense by entering the amount, category, date, and notes.
   - **Set Category Budget**: Set budget limits for specific categories.
   - **View Monthly Summary**: View a summary of total income, expenses, and balance.
   - **View Expense Breakdown by Category**: See expenses per category and any budget overages.
   - **Save and Exit**: Save all data to `finance_data.json` and exit the program.

## Example Usage

```plaintext
Set your monthly budget: 2000

Personal Finance Manager
1. Add Income
2. Add Expense
3. Set Category Budget
4. View Monthly Summary
5. View Expense Breakdown by Category
6. Save and Exit
Select an option: 1

Enter income amount: 1000
Enter income date (yyyy-mm-dd): 2024-01-01
Enter income source: Salary
Income added successfully.

## Data Persistence

All data is saved to `finance_data.json` in the project root directory. This file will load automatically when you reopen the program, so previous data remains accessible.


## Acknowledgments

This project was developed as part of a personal learning experience with C# and console-based application development. Contributions and feedback are welcome!

Feel free to modify any section, such as the "Acknowledgments"  based on your needs.
