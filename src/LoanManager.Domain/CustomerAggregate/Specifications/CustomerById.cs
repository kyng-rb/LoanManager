using Ardalis.Specification;

namespace LoanManager.Domain.CustomerAggregate.Specifications;

public sealed class CustomerById : Specification<Customer>
{
    public CustomerById(int id)
    {
        Query
            .Where(customer => customer.Id == id);
    }
}