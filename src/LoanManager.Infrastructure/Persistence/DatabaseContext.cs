using LoanManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace LoanManager.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
                    .HasMany(x => x.Loans)
                    .WithOne(s => s.Owner)
                    .HasForeignKey(q => q.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Person>()
                    .HasMany(x => x.Lends)
                    .WithOne(s => s.Customer)
                    .HasForeignKey(q => q.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
    }

    public virtual DbSet<Loan> Loans => Set<Loan>();
    public virtual DbSet<Transaction> Transactions  => Set<Transaction>();
    public virtual DbSet<Warranty> Warranties  => Set<Warranty>();
    public virtual DbSet<User> Users  => Set<User>();
    public virtual DbSet<Person> Persons => Set<Person>();
}