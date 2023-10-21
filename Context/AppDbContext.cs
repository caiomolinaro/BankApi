using BankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<BankAccountModel> BankAccount { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankAccountModel>()
            .HasIndex(e => e.EmailClient)
            .IsUnique();

        modelBuilder.Entity<BankAccountModel>()
            .HasIndex(e => e.DocumentClient)
            .IsUnique();

        modelBuilder.Entity<BankAccountModel>()
            .HasIndex(e => e.PhoneClient)
            .IsUnique();
    }

}

