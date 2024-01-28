using ErrorOr;

namespace LoanManager.Application.Common.Interfaces.Persistence;

public interface ICustomerRepository
{
    void Add(Domain.Entities.Customer customer);

    ErrorOr<Domain.Entities.Customer> GetByPhone(string phone);
}