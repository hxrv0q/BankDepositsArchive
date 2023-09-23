using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDeposits.App.Models;

[Table("Depositor")]
public class Depositor
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }

    [Required, MaxLength(50)] public string LastName { get; init; } = null!;

    [Required, MaxLength(50)] public string FirstName { get; set; } = null!;

    [MaxLength(50)] public string? Patronymic { get; init; }

    [Required, MaxLength(10)] public string PassportSeries { get; init; } = null!;

    [Required, MaxLength(10)] public string PassportNumber { get; init; } = null!;

    [Required, MaxLength(255)] public string HomeAddress { get; init; } = null!;

    public List<Account> Accounts { get; init; } = new();
}