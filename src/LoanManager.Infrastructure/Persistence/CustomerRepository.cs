using ErrorOr;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Domain.Common.Errors;
using LoanManager.Domain.Entities;

namespace LoanManager.Infrastructure.Persistence;

public class CustomerRepository : ICustomerRepository
{
    private readonly static List<Customer> Customers = new();
    private readonly DatabaseContext _context;

    public CustomerRepository(DatabaseContext context)
    {
        _context = context;
    }

    public void Add(Customer customer)
    {
        Customers.Add(customer);
    }

    public void Update(Customer customer)
    {
        _context.Customers.Attach(customer);
    }

    public bool ExistsByPhone(string phone) 
        => Customers.Exists(customer => customer.Phone.Equals(phone, StringComparison.InvariantCultureIgnoreCase));

    public ErrorOr<Customer> GetById(int customerId)
    {
        var customer = Customers.SingleOrDefault(customer => customer.Id == customerId);
        
        if (customer is null)
            return Errors.Customer.NotFound;

        return customer;
    }
}