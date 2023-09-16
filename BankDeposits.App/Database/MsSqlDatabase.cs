using System.Data;
using Microsoft.Data.SqlClient;

namespace BankDeposits.App.Database;

public class MsSqlDatabase : AbstractDatabase<SqlConnection, SqlCommand, SqlDataAdapter>
{
    public MsSqlDatabase(string connectionString) : base(connectionString)
    {
    }
}