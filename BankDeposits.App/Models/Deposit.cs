using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankDeposits.App.Models;

[Table("Deposit")]
public class Deposit
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }

    [Required] public Guid AccountId { get; init; }

    [Column(TypeName = "money"), Required] public decimal Amount { get; init; }

    [Required] public DateTime Date { get; init; }

    [ForeignKey(nameof(AccountId)), JsonIgnore]
    public Account Account { get; init; } = null!;
}