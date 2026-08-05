# Debugging Notes — BankAccountLibrary

Sessions below were run using the VS Code Test Explorer "Debug Test" action against
`BankAccountLibrary.Tests`, stepping through `BankAccount.cs` with breakpoints, the Locals
window, and Watch expressions.

## Session 1 — Withdrawal declined below minimum balance

Test: `Withdraw_BelowMinimumBalance_ReturnsFalseAndBalanceUnchanged`
(`new BankAccount("ACC-4000", "Sam Rivera", 100m, minimumBalance: 20m)`, then `Withdraw(90m)`)

Breakpoint: first line of `Withdraw(decimal amount)`.

| Step | Location | Locals observed |
|---|---|---|
| 1 | Entry to `Withdraw` | `amount = 90`, `this.Balance = 100`, `this.MinimumBalance = 20` |
| 2 | After `ValidateTransactionAmount(amount)` call | `error = null` (90 is a valid positive amount) |
| 3 | At `if (Balance - amount < MinimumBalance)` | Watch `Balance - amount` evaluates to `10`; `10 < 20` is `true` |
| 4 | Step into the `return false;` branch | Confirms `_transactionHistory` is never touched — count stays at 1 (account-opened entry only) |
| 5 | Back in test | `result == false`, `account.Balance == 100` (unchanged) — matches assertion |

## Session 2 — Successful withdrawal down to exactly the minimum

Test: `Withdraw_DownToExactMinimumBalance_Succeeds`
(same account setup, then `Withdraw(80m)`)

| Step | Location | Locals observed |
|---|---|---|
| 1 | At `if (Balance - amount < MinimumBalance)` | `Balance - amount = 20`, `MinimumBalance = 20` → condition `20 < 20` is `false`, so the withdrawal proceeds |
| 2 | After `Balance -= amount;` | `Balance` updates from `100` to `20` |
| 3 | After `_transactionHistory.Add(...)` | History count goes from 1 to 2, new entry is `"Withdrew $80.00"` |
| 4 | Return | `result == true` |

This boundary case is the reason the condition uses `<` rather than `<=` — stepping through it
confirmed a withdrawal landing exactly on `MinimumBalance` is intentionally allowed.

## Session 3 — Extracted pure validation helper

Test: `ValidateTransactionAmount_ZeroAmount_ReturnsErrorMessage`

Breakpoint: inside `ValidateTransactionAmount(decimal amount)`.

| Step | Location | Locals observed |
|---|---|---|
| 1 | Entry | `amount = 0` |
| 2 | Ternary evaluation `amount <= 0 ? "..." : null` | `amount <= 0` is `true` |
| 3 | Return | Returns the error string, confirmed via Watch: not `null` |

Because this method takes no dependency on account state, stepping through it in isolation (no
`this`, no account instance) made it trivial to confirm every branch — this was the direct
motivation for extracting it out of `Deposit`/`Withdraw` in DCR-1 (see `DCR.md`).

## Session 4 — Constructor rejecting an inconsistent minimum balance

Test: `Constructor_InitialBalanceBelowMinimumBalance_ThrowsArgumentException`
(`new BankAccount("ACC-4002", "Sam Rivera", 10m, minimumBalance: 20m)`)

| Step | Location | Locals observed |
|---|---|---|
| 1 | At `if (minimumBalance < 0)` | `minimumBalance = 20` → condition `false`, skipped |
| 2 | At `if (initialBalance < minimumBalance)` | `initialBalance = 10`, `minimumBalance = 20` → `10 < 20` is `true` |
| 3 | Step into `throw new ArgumentException(...)` | Exception propagates before `AccountNumber`/`Balance`/etc. are ever assigned — confirms the object is never left in a half-constructed invalid state |

## Debugging tools used

- Breakpoints on the first executable line of each method under test.
- Step Into (F11) to move from the calling test into `BankAccount` methods.
- Locals window to observe `amount`, `Balance`, `MinimumBalance`, `error` at each step.
- Watch expressions for derived values not directly in scope (e.g. `Balance - amount`).
- Test Explorer's "Debug Test" launch (rather than a full app run) since the class has no UI —
  every code path is reachable directly from a unit test.
