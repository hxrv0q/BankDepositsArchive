using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDeposits.Mvc.Models;

[Table("Deposit")]
public class Deposit : IdentifierEntity
{
    [Required]
    public Guid AccountId { get; set; }

    [ForeignKey(nameof(AccountId))]
    [NotMapped]
    public Account Account { get; set; } = null!;

    [Column(TypeName = "money"), Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTime Date { get; set; }
}