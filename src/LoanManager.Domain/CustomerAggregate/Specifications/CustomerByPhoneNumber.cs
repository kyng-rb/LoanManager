using Ardalis.Specification;

namespace LoanManager.Domain.CustomerAggregate.Specifications;

public sealed class CustomerByPhoneNumber : Specification<Customer>
{
    public CustomerByPhoneNumber(string phoneNumber)
    {
        Query
            .Where(customer => customer.Phone.Equals(phoneNumber, StringComparison.InvariantCultureIgnoreCase));
    }
}