using ErrorOr;

using MediatR;

namespace LoanManager.Application.Transactions.Queries.Loans;

public record RecordsQuery(
string CustomerName,
DateOnly? StartDate,
DateOnly? EndDate) : IRequest<ErrorOr<LoansQueryResult>>;