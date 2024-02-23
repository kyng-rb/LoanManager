using ErrorOr;
using LoanManager.Application.Customer.Common;
using MediatR;

namespace LoanManager.Application.Customer.Queries.Retrieve;

public record Query(string Search) : IRequest<ErrorOr<QueryResult>>;