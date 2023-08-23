using LoanManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace LoanManager.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public virtual DbSet<Server> Servers => Set<Server>();
    public virtual DbSet<ServerUser> ServerUser => Set<ServerUser>();
    public virtual DbSet<ServerCustomer> ServerCustomers => Set<ServerCustomer>();
    public virtual DbSet<Customer> Customers => Set<Customer>();
    public virtual DbSet<User> Users => Set<User>();

    public virtual DbSet<Loan> Loans => Set<Loan>();
    public virtual DbSet<Transaction> Transactions => Set<Transaction>();
    public virtual DbSet<Withheld> Withhelds => Set<Withheld>();

}