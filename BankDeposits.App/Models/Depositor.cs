using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDeposits.App.Models;

[Table("Depositor")]
public class Depositor
{
    [Key, Column("ID")] public Guid Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;
    
    public string PassportSeries { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

    public string HomeAddress { get; set; } = null!;

    public List<Account> Accounts { get; set; } = new();
}