using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDeposits.Mvc.Models;

[Table("Account")]
public class Account : IdentityEntity
{
    [Required]
    public Guid DepositorId { get; set; }

    [ForeignKey(nameof(DepositorId))]
    [NotMapped]
    public Depositor? Depositor { get; set; }

    [Required, MaxLength(20)]
    public string Number { get; set; } = null!;

    [Column(TypeName = "money"), Required]
    public decimal Amount { get; set; }

    [NotMapped]
    public ICollection<Deposit>? Deposits { get; set; }
}