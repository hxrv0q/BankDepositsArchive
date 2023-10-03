using System.ComponentModel.DataAnnotations;

namespace BankDeposits.Mvc.Models;

public abstract class IdentifierEntity
{
    [Key]
    public Guid Id { get; set; }
}