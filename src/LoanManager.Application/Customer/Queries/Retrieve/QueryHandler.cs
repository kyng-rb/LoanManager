using Ardalis.Specification;
using ErrorOr;
using LoanManager.Application.Customer.Common;
using LoanManager.Domain.CustomerAggregate.Specifications;
using MediatR;

namespace LoanManager.Application.Customer.Queries.Retrieve;

public class QueryHandler(IRepositoryBase<Domain.CustomerAggregate.Customer> repository) 
    : IRequestHandler<Query, ErrorOr<QueryResult>>
{
    private readonly IRepositoryBase<Domain.CustomerAggregate.Customer> _repository = repository;

    public async Task<ErrorOr<QueryResult>> Handle(Query request, CancellationToken cancellationToken)
    {
        var customerSearchSpecification = new CustomerSearch(request.Search);
        var records = await _repository.ListAsync(customerSearchSpecification, cancellationToken);

        if (records.Count == 0)
        {
            return new QueryResult(Array.Empty<Common.Customer>());
        }

        var customers = Common.Customer.FromEntities(records.ToArray());
        return new QueryResult(customers);
    }
}