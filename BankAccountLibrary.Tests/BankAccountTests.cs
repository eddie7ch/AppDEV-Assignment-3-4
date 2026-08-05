using System;
using NUnit.Framework;
using BankAccountLibrary;

namespace BankAccountLibrary.Tests
{
    public class BankAccountTests
    {
        private BankAccount _account;

        [SetUp]
        public void Setup()
        {
            _account = new BankAccount("ACC-1001", "  john DOE  ", 100m);
        }

        // ----- Normal usage flow -----

        [Test]
        public void Constructor_NormalizesAccountHolderName()
        {
            Assert.That(_account.AccountHolderName, Is.EqualTo("John Doe"));
        }

        [Test]
        public void Deposit_IncreasesBalance()
        {
            _account.Deposit(50m);
            Assert.That(_account.Balance, Is.EqualTo(150m));
        }

        [Test]
        public void Withdraw_WithSufficientFunds_ReturnsTrueAndDecreasesBalance()
        {
            bool result = _account.Withdraw(40m);

            Assert.That(result, Is.True);
            Assert.That(_account.Balance, Is.EqualTo(60m));
        }

        [Test]
        public void GetTransactionCount_ReflectsDepositsAndWithdrawals()
        {
            _account.Deposit(10m);
            _account.Withdraw(5m);

            // 1 for account opening + deposit + withdrawal
            Assert.That(_account.GetTransactionCount(), Is.EqualTo(3));
        }

        [Test]
        public void GetAccountSummary_ReturnsFormattedString()
        {
            string summary = _account.GetAccountSummary();
            Assert.That(summary, Does.Contain("ACC-1001"));
            Assert.That(summary, Does.Contain("John Doe"));
        }

        [Test]
        public void CalculateMonthlyInterest_ReturnsExpectedFloat()
        {
            float interest = _account.CalculateMonthlyInterest(12f);
            Assert.That(interest, Is.EqualTo(1.0f).Within(0.0001f));
        }

        [Test]
        public void GetTransactionHistory_ReturnsAllRecordedTransactions()
        {
            _account.Deposit(25m);

            var history = _account.GetTransactionHistory();

            Assert.That(history.Count, Is.EqualTo(2));
            Assert.That(history[1], Does.Contain("Deposited"));
        }

        // ----- Edge cases -----

        [Test]
        public void Withdraw_ExactBalance_SucceedsAndZeroesBalance()
        {
            bool result = _account.Withdraw(100m);

            Assert.That(result, Is.True);
            Assert.That(_account.Balance, Is.EqualTo(0m));
        }

        [Test]
        public void Withdraw_MoreThanBalance_ReturnsFalseAndBalanceUnchanged()
        {
            bool result = _account.Withdraw(1000m);

            Assert.That(result, Is.False);
            Assert.That(_account.Balance, Is.EqualTo(100m));
        }

        [Test]
        public void Constructor_ZeroInitialBalance_IsAllowed()
        {
            var account = new BankAccount("ACC-2000", "Jane Smith", 0m);
            Assert.That(account.Balance, Is.EqualTo(0m));
        }

        // ----- Incorrect values -----

        [Test]
        public void Deposit_NegativeAmount_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _account.Deposit(-10m));
        }

        [Test]
        public void Deposit_ZeroAmount_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _account.Deposit(0m));
        }

        [Test]
        public void Withdraw_NegativeAmount_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _account.Withdraw(-5m));
        }

        [Test]
        public void CalculateMonthlyInterest_NegativeRate_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _account.CalculateMonthlyInterest(-1f));
        }

        // ----- Exceptions (constructor validation) -----

        [Test]
        public void Constructor_NegativeInitialBalance_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BankAccount("ACC-3000", "Alex Lee", -1m));
        }

        [Test]
        public void Constructor_EmptyAccountNumber_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new BankAccount("", "Alex Lee", 10m));
        }

        // ----- Empty string / incorrect string -----

        [Test]
        public void NormalizeAccountHolderName_EmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => BankAccount.NormalizeAccountHolderName(""));
        }

        [Test]
        public void NormalizeAccountHolderName_WhitespaceString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => BankAccount.NormalizeAccountHolderName("   "));
        }

        [Test]
        public void NormalizeAccountHolderName_NullString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => BankAccount.NormalizeAccountHolderName(null!));
        }

        [Test]
        public void NormalizeAccountHolderName_MixedCaseWithExtraSpaces_ReturnsTitleCase()
        {
            string result = BankAccount.NormalizeAccountHolderName("   sArAh CONNOR   ");
            Assert.That(result, Is.EqualTo("Sarah Connor"));
        }
    }
}
