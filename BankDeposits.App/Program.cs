using System.Data;
using BankDeposits.App.Configuration;
using BankDeposits.App.Database;
using Microsoft.Extensions.Configuration;

var configuration = AppConfig.LoadConfiguration();
var connectionString = configuration.GetConnectionString("BankDepositsDatabase")
                       ?? throw new Exception("Connection string is not found");

try
{
    var database = new MsSqlDatabase(connectionString);

    const string query =
        """
        SELECT TOP 10 d.ID, d.LastName, d.FirstName, d.Patronymic, COUNT(de.ID) AS VisitCount
        FROM Depositor d
            INNER JOIN Account a ON a.DepositorID = d.ID
            INNER JOIN Deposit de ON de.AccountID = a.ID
        GROUP BY d.ID, d.LastName, d.FirstName, d.Patronymic
        HAVING COUNT(de.ID) > 1
        ORDER BY VisitCount DESC
        """;

    var data = database.ExecuteQuery(query);
    var table = data.Tables[0];

    foreach (DataRow row in table.Rows)
    {
        Console.WriteLine(string.Join(", ", row.ItemArray));
    }
}
catch (Exception e)
{
    Console.Error.WriteLine(e);
}