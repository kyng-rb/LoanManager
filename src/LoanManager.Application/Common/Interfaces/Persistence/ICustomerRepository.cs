using ErrorOr;

namespace LoanManager.Application.Common.Interfaces.Persistence;

public interface ICustomerRepository
{
    void Add(Domain.Entities.Customer customer);
    void Update(Domain.Entities.Customer customer);
    bool ExistsByPhone(string phone);
    ErrorOr<Domain.Entities.Customer> GetById(int customerId);
}