using Microsoft.EntityFrameworkCore;

namespace BankDeposits.Mvc.Models;

public class BankDepositsContext : DbContext
{
    public BankDepositsContext(DbContextOptions<BankDepositsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Depositor>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Depositor>().HasIndex(d => new { d.PassportSeries, d.PassportNumber }).IsUnique();

        modelBuilder.Entity<Account>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Deposit>().Property(p => p.Id).ValueGeneratedOnAdd();
    }


    public DbSet<Depositor> Depositors { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Deposit> Deposits { get; set; } = null!;
}