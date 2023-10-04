using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDeposits.Mvc.Models;

[Table("Account")]
public class Account : IdentifierEntity
{
    [Required]
    public Guid DepositorId { get; set; }

    [ForeignKey(nameof(DepositorId))]
    [NotMapped]
    public Depositor Depositor { get; set; } = null!;

    [Required, MaxLength(20)]
    public string AccountNumber { get; set; } = null!;

    [Column(TypeName = "money"), Required]
    public decimal Amount { get; set; }

    [NotMapped]
    public ICollection<Deposit>? Deposits { get; set; }
}