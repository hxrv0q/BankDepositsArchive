using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Models;

public class BankDepositsContext : DbContext
{
    public BankDepositsContext(DbContextOptions<BankDepositsContext> options) : base(options)
    {
    }

    public DbSet<Depositor> Depositors { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Deposit> Deposits { get; set; } = null!;
}