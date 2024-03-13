using Ardalis.Specification;

namespace LoanManager.Domain.CustomerAggregate.Specifications;

public sealed class CustomerSearch : Specification<Customer>
{
    public CustomerSearch(string search)
    {
        Query
            .Search(customer => customer.FirstName, search);
    }
}