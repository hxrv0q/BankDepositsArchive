using System.ComponentModel.DataAnnotations;

namespace BankDeposits.Mvc.Models;

public class IdentifierEntity
{
    [Key]
    public Guid Id { get; set; }
}