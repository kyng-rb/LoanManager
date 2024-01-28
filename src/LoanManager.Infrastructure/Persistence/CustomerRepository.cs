using ErrorOr;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Domain.Common.Errors;
using LoanManager.Domain.Entities;

namespace LoanManager.Infrastructure.Persistence;

public class CustomerRepository :ICustomerRepository
{
    private readonly static List<Customer> Customers = new();
    
    public void Add(Customer customer)
    {
        Customers.Add(customer);
    }

    public ErrorOr<Customer> GetByPhone(string phone)
    {
        var customer = Customers.SingleOrDefault(customer =>
                                                     customer.Phone.Equals(phone,
                                                                           StringComparison
                                                                               .InvariantCultureIgnoreCase));
        if (customer is null)
            return Errors.Customer.NotFound;

        return customer;
    }
}