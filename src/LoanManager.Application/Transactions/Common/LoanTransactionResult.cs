using LoanManager.Domain.Enums;

namespace LoanManager.Application.Transactions.Common;

public record LoanTransactionResult(
int LoanId,
Currency Currency,
decimal Amount,
float InterestRate,
DateOnly Date,
int CustomerId);