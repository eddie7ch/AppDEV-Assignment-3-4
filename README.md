# AppDEV Assignment 3 and 4

Unit testing assignment (Module 3): a `BankAccount` class with NUnit test coverage.

## Projects

- `BankAccountLibrary` - the `BankAccount` class:
  - `NormalizeAccountHolderName(string)` - string processing, returns a `string`
  - `Withdraw(decimal)` - returns a `bool`
  - `GetTransactionCount()` - returns an `int`
  - `CalculateMonthlyInterest(float)` - returns a `float`
  - `GetTransactionHistory()` - returns a `List<string>`
- `BankAccountLibrary.Tests` - NUnit tests covering normal usage, edge cases,
  incorrect values, exceptions, and empty/incorrect strings.

## Run tests

```
dotnet test
```
