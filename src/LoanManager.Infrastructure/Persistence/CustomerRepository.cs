using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Domain.Entities;

namespace LoanManager.Infrastructure.Persistence;

public class CustomerRepository(DatabaseContext context) : ICustomerRepository
{
    private readonly static List<Customer> Customers = new();
    private readonly DatabaseContext _context = context;

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

    public bool ExistsById(int customerId)
        => Customers.Exists(customer => customer.Id == customerId);

    public Customer[] Get(string? search)
    {
        if (search is null)
            return Customers.ToArray();

        return Customers.Where(x => x.Phone.Contains(search) ||
                             x.FirstName.Contains(search) ||
                             (x.LastName is not null && x.LastName.Contains(search)))
            .ToArray();
    }
}