namespace LoanManager.Application.Common.Interfaces.Persistence;

public interface ICustomerRepository
{
    void Add(Domain.Entities.Customer customer);
    void Update(Domain.Entities.Customer customer);
    bool ExistsByPhone(string phone);
    bool ExistsById(int customerId);
    Domain.Entities.Customer[] Get(string? search);
}