using System.Data;
using BankDeposits.App.Configuration;
using BankDeposits.App.Database;
using Microsoft.Extensions.Configuration;

var configuration = AppConfig.LoadConfiguration();
var connectionString = configuration.GetConnectionString("BankDepositsDatabase");

if (connectionString != null)
{
    try
    {

        IDatabase database = new MsSqlDatabase(connectionString);

        const string query = """
                             SELECT TOP 10 d.ID, d.LastName, d.FirstName, d.Patronymic, COUNT(de.ID) AS VisitCount
                                         FROM Depositor d
                                         JOIN Account a ON d.ID = a.DepositorID
                                         JOIN Deposit de ON a.ID = de.AccountID
                                         GROUP BY d.ID , d.LastName, d.FirstName, d.Patronymic
                                         HAVING COUNT(de.ID) > 2;
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
        Console.WriteLine(e);
    }
}