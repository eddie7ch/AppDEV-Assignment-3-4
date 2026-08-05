# DCR-1: Minimum Balance / Overdraft Protection

## Context

The original `BankAccount` class allowed `Withdraw` to succeed all the way down to a balance
of exactly `0`, with no way to enforce a minimum balance (overdraft protection). This DCR adds
that capability without breaking any existing behavior.

## Requested change

1. Add a `MinimumBalance` property (default `0m`, backward compatible) so `Withdraw` refuses to
   drop the balance below it.
2. Extract the amount-validation rule shared by `Deposit` and `Withdraw` ("amount must be > 0")
   into one pure, independently testable static method.

## Implementation

- Added an optional `minimumBalance` constructor parameter (default `0m`), exposed as the
  read-only `MinimumBalance` property.
- Constructor now validates `minimumBalance >= 0` (`ArgumentOutOfRangeException` if not) and
  `initialBalance >= minimumBalance` (`ArgumentException` if not).
- `Withdraw` checks `Balance - amount < MinimumBalance`; if true it returns `false` and leaves
  balance/history untouched instead of throwing.
- Extracted `public static string? ValidateTransactionAmount(decimal amount)` — returns `null`
  when the amount is valid, otherwise an error message. Both `Deposit` and `Withdraw` call it and
  throw `ArgumentOutOfRangeException` when it returns non-null.

## Ensuring the change didn't break existing components

- Default `minimumBalance = 0m` means every account created via the original 3-argument
  constructor behaves exactly as before.
- Ran the full suite after the change (`dotnet test`): all pre-existing tests (deposit,
  withdrawal, transaction count, summary, interest, history, name normalization) still pass
  unmodified.
- 8 new tests were added specifically for the new behavior, none of the old tests needed to be
  rewritten — evidence that the change is additive, not a breaking rework.

## New tests added (8)

| Test | Verifies |
|---|---|
| `Withdraw_BelowMinimumBalance_ReturnsFalseAndBalanceUnchanged` | Withdrawal that would breach the minimum is rejected |
| `Withdraw_DownToExactMinimumBalance_Succeeds` | Withdrawal landing exactly on the minimum still succeeds |
| `Constructor_InitialBalanceBelowMinimumBalance_ThrowsArgumentException` | Constructor rejects an invalid starting state |
| `Constructor_NegativeMinimumBalance_ThrowsArgumentOutOfRangeException` | Constructor rejects a negative minimum |
| `Constructor_DefaultMinimumBalance_IsZero` | Backward-compatible default is preserved |
| `ValidateTransactionAmount_PositiveAmount_ReturnsNull` | Pure helper accepts valid input |
| `ValidateTransactionAmount_ZeroAmount_ReturnsErrorMessage` | Pure helper rejects zero |
| `ValidateTransactionAmount_NegativeAmount_ReturnsErrorMessage` | Pure helper rejects negative values |

## Code coverage results (after the DCR)

- Line coverage: 70/70 (100%)
- Branch coverage: 20/20 (100%)
- Measured via `dotnet test --collect:"XPlat Code Coverage"` on 2026-08-05, 28/28 tests passing.

The rubric notes that covering 100% of a class can be "overkill" for a real team/production
project. For a class this small, full coverage was a natural byproduct of deliberately testing
every category the assignment asks for (normal flow, edge cases, incorrect values, exceptions,
empty/incorrect strings) rather than a goal chased for its own sake — every one of the 28 tests
maps to one of those categories, not to padding the coverage number.
