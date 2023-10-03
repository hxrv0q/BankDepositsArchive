using MongoDB.Bson.Serialization.Attributes;

namespace BankDeposits.App.Models;

/// <summary>
/// Represents a bank account.
/// </summary>
public class Account
{
    public string AccountNumber { get; init; } = null!;

    public decimal Amount { get; init; }

    [BsonElement("Deposits")]
    public List<Deposit> Deposits { get; init; } = new();
}