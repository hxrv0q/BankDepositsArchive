using BankDeposits.App.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.App.Database;

/// <summary>
/// Represents a database context for the application.
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<Depositor> Depositors { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Deposit> Deposits { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}