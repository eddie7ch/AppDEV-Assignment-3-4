using System;
using System.Collections.Generic;
using System.Globalization;

namespace BankAccountLibrary
{
    /// <summary>
    /// Represents a simple bank account with deposit, withdrawal and interest calculation.
    /// </summary>
    public class BankAccount
    {
        private readonly List<string> _transactionHistory = new List<string>();

        public string AccountNumber { get; }
        public string AccountHolderName { get; }
        public decimal Balance { get; private set; }

        public BankAccount(string accountNumber, string accountHolderName, decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("Account number cannot be null or empty.", nameof(accountNumber));
            }

            if (initialBalance < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance cannot be negative.");
            }

            AccountNumber = accountNumber;
            AccountHolderName = NormalizeAccountHolderName(accountHolderName);
            Balance = initialBalance;
            _transactionHistory.Add($"Account opened with balance {initialBalance:C}");
        }

        /// <summary>
        /// String processing function: trims whitespace and converts a name to Title Case.
        /// </summary>
        public static string NormalizeAccountHolderName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Account holder name cannot be null, empty, or whitespace.", nameof(name));
            }

            string trimmed = name.Trim().ToLower(CultureInfo.InvariantCulture);
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(trimmed);
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be greater than zero.");
            }

            Balance += amount;
            _transactionHistory.Add($"Deposited {amount:C}");
        }

        /// <summary>
        /// Returns true if the withdrawal succeeded, false if funds were insufficient.
        /// </summary>
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be greater than zero.");
            }

            if (amount > Balance)
            {
                return false;
            }

            Balance -= amount;
            _transactionHistory.Add($"Withdrew {amount:C}");
            return true;
        }

        public int GetTransactionCount()
        {
            return _transactionHistory.Count;
        }

        /// <summary>
        /// Calculates the simple monthly interest for the current balance given an annual rate percentage.
        /// </summary>
        public float CalculateMonthlyInterest(float annualInterestRatePercent)
        {
            if (annualInterestRatePercent < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(annualInterestRatePercent), "Interest rate cannot be negative.");
            }

            return (float)Balance * annualInterestRatePercent / 100f / 12f;
        }

        public List<string> GetTransactionHistory()
        {
            return new List<string>(_transactionHistory);
        }

        public string GetAccountSummary()
        {
            return $"Account: {AccountNumber} | Holder: {AccountHolderName} | Balance: {Balance:C}";
        }
    }
}
