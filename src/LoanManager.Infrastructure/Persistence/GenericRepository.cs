using Ardalis.Specification.EntityFrameworkCore;
using LoanManager.Domain.Aggregate;

namespace LoanManager.Infrastructure.Persistence;

public class GenericRepository<T>(DatabaseContext dbContext) : RepositoryBase<T>(dbContext)
    where T : BaseAggregate;