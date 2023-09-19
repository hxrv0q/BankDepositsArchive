using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic.CompilerServices;

namespace BankDeposits.App.Models;

[Table("Account")]
public class Account
{
    [Key, Column("ID")] public Guid Id { get; set; }

    [Column("DepositorID")] public Guid DepositorId { get; set; }

    public string AccountNumber { get; set; } = null!;

    [Column(TypeName = "money")] public decimal Amount { get; set; }

    public Depositor Depositor { get; set; } = null!;

    public List<Deposit> Deposits { get; set; } = new();
}