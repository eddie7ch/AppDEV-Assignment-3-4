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

        /// <summary>
        /// The lowest balance this account is allowed to drop to via <see cref="Withdraw"/>.
        /// Added by DCR-1 to support minimum-balance / overdraft-protected accounts.
        /// </summary>
        public decimal MinimumBalance { get; }

        public BankAccount(string accountNumber, string accountHolderName, decimal initialBalance, decimal minimumBalance = 0m)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("Account number cannot be null or empty.", nameof(accountNumber));
            }

            if (initialBalance < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance cannot be negative.");
            }

            if (minimumBalance < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumBalance), "Minimum balance cannot be negative.");
            }

            if (initialBalance < minimumBalance)
            {
                throw new ArgumentException("Initial balance cannot be less than the minimum balance.", nameof(initialBalance));
            }

            AccountNumber = accountNumber;
            AccountHolderName = NormalizeAccountHolderName(accountHolderName);
            Balance = initialBalance;
            MinimumBalance = minimumBalance;
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
            string? error = ValidateTransactionAmount(amount);
            if (error is not null)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), error);
            }

            Balance += amount;
            _transactionHistory.Add($"Deposited {amount:C}");
        }

        /// <summary>
        /// Returns true if the withdrawal succeeded, false if it would breach the minimum balance.
        /// </summary>
        public bool Withdraw(decimal amount)
        {
            string? error = ValidateTransactionAmount(amount);
            if (error is not null)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), error);
            }

            if (Balance - amount < MinimumBalance)
            {
                return false;
            }

            Balance -= amount;
            _transactionHistory.Add($"Withdrew {amount:C}");
            return true;
        }

        /// <summary>
        /// Pure validation helper extracted by DCR-1 so deposit/withdrawal amount rules can be
        /// unit tested independently of any account instance or state mutation.
        /// Returns null when the amount is valid, otherwise an error message.
        /// </summary>
        public static string? ValidateTransactionAmount(decimal amount)
        {
            return amount <= 0 ? "Transaction amount must be greater than zero." : null;
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
