using Ardalis.Specification.EntityFrameworkCore;
using LoanManager.Domain.Aggregate;
using Microsoft.EntityFrameworkCore;

namespace LoanManager.Infrastructure.Persistence;

public class GenericRepository<T>(DbContext dbContext) : RepositoryBase<T>(dbContext)
    where T : BaseAggregate;