using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankDeposits.App.Models;

/// <summary>
/// Represents a bank account.
/// </summary>
[Table("Account")]
public class Account
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }

    [Required] public Guid DepositorId { get; init; }

    [Required, MaxLength(20)] public string AccountNumber { get; init; } = null!;

    [Column(TypeName = "money"), Required] public decimal Amount { get; init; }

    [ForeignKey(nameof(DepositorId)), JsonIgnore]
    public Depositor Depositor { get; init; } = null!;

    public List<Deposit> Deposits { get; init; } = new();
}
