namespace BankDeposits.App.Models;

/// <summary>
/// Represents a deposit transaction.
/// </summary>
public class Deposit
{
    public decimal Amount { get; init; }

    public DateTime Date { get; init; }
}