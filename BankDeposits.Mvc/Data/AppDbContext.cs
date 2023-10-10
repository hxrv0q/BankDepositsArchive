using BankDeposits.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }


    public DbSet<Depositor> Depositors { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Deposit> Deposits { get; set; } = null!;
}