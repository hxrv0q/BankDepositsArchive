using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDeposits.App.Models;

[Table("Deposit")]
public class Deposit
{
    [Key, Column("ID")] public Guid Id { get; set; }

    [Column("AccountID")] public Guid AccountId { get; set; }

    [Column(TypeName = "money")] public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    [ForeignKey(nameof(AccountId))]
    public Account Account { get; set; } = null!;
}