using ErrorOr;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Application.Customer.Common;
using MediatR;

namespace LoanManager.Application.Customer.Queries.Retrieve;

public class QueryHandler(ICustomerRepository repository) 
    : IRequestHandler<Query, ErrorOr<QueryResult>>
{
    private readonly ICustomerRepository _repository = repository;

    public async Task<ErrorOr<QueryResult>> Handle(Query request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var records = _repository.Get(request.Search);

        if (records.Length == 0)
        {
            return new QueryResult(Array.Empty<Common.Customer>());
        }

        var customers = Common.Customer.FromEntities(records);
        return new QueryResult(customers);
    }
}