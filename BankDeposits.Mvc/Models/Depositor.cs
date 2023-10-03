using BankDeposits.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDeposits.Mvc.Models;

[Table("Depositor")]
public class Depositor : IdentifierEntity
{
    [Required, MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(50)]
    public string Patronymic { get; set; } = null!;

    [Required, MaxLength(10)]
    public string PassportSeries { get; set; } = null!;

    [Required, MaxLength(10)]
    public string PassportNumber { get; set; } = null!;

    [Required, MaxLength(255)]
    public string HomeAddress { get; set; } = null!;

    [ExcludeInView]
    public ICollection<Account>? Accounts { get; set; }
}