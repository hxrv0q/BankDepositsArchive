using BankDeposits.App.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.App.Database;

public class AppDbContext : DbContext
{

    public DbSet<Depositor> Depositors { get; init; } = null!;
    public DbSet<Account> Accounts { get; init; } = null!;
    public DbSet<Deposit> Deposits { get; init; } = null!;


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureDepositorEntity(modelBuilder);
        ConfigureAccountEntity(modelBuilder);
        ConfigureDepositEntity(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureDepositorEntity(ModelBuilder modelBuilder) => modelBuilder.Entity<Depositor>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
        entity.Property(e => e.Patronymic).HasMaxLength(50);
        entity.Property(e => e.PassportSeries).IsRequired().HasMaxLength(10);
        entity.Property(e => e.PassportNumber).IsRequired().HasMaxLength(10);
        entity.Property(e => e.HomeAddress).IsRequired().HasMaxLength(255);
    });

    private static void ConfigureAccountEntity(ModelBuilder modelBuilder) => modelBuilder.Entity<Account>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.AccountNumber).IsRequired().HasMaxLength(20);
        entity.Property(e => e.Amount).IsRequired().HasColumnType("money");
        entity.HasOne(e => e.Depositor).WithMany(d => d.Accounts).HasForeignKey(e => e.DepositorId)
            .OnDelete(DeleteBehavior.Cascade);
    });

    private static void ConfigureDepositEntity(ModelBuilder modelBuilder) => modelBuilder.Entity<Deposit>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Amount).IsRequired().HasColumnType("money");
        entity.Property(e => e.Date).IsRequired().HasColumnType("datetime");
        entity.HasOne(e => e.Account).WithMany(a => a.Deposits).HasForeignKey(e => e.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
    });
}