using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BankDeposits.App.Models;

/// <summary>
/// Represents a depositor.
/// </summary>
public class Depositor
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Patronymic { get; init; }

    public string PassportSeries { get; init; } = null!;

    public string PassportNumber { get; init; } = null!;

    public string HomeAddress { get; init; } = null!;

    [BsonElement("Accounts")] public List<Account> Accounts { get; init; } = new();
}