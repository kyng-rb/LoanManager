using ErrorOr;

using LoanManager.Application.Transactions.Common;
using LoanManager.Domain.Enums;

using MediatR;

namespace LoanManager.Application.Transactions.Commands.RegisterLoan;

public record RegisterLoanCommand(
Currency Currency,
decimal Amount,
float InterestRate,
DateOnly Date,
int CustomerId) : IRequest<ErrorOr<LoanTransactionResult>>;