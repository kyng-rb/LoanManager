namespace LoanManager.Application.Customer.Common;

public record Customer(int Id, string FirstName, string? LastName, string Phone)
{
    public static Customer FromEntity(Domain.CustomerAggregate.Customer entity)
    {
        return new Customer(entity.Id, entity.FirstName, entity.LastName, entity.Phone);
    }
    
    public static Customer[] FromEntities(Domain.CustomerAggregate.Customer[] entities)
    {
        return entities.Select(FromEntity).ToArray();
    }
}