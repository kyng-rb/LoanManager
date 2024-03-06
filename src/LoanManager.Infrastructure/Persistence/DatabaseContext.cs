using LoanManager.Domain.CustomerAggregate;
using LoanManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace LoanManager.Infrastructure.Persistence;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public virtual DbSet<Customer> Customers => Set<Customer>();
    public virtual DbSet<User> Users => Set<User>();
    public virtual DbSet<Loan> Loans => Set<Loan>();
    public virtual DbSet<Transaction> Transactions => Set<Transaction>();
    public virtual DbSet<Withhold> Withholds => Set<Withhold>();

}