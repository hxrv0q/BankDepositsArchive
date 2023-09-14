using System.Data;
using Microsoft.Data.SqlClient;

namespace BankDeposits.App.Database;

public class MsSqlDatabase : IDatabase
{
    private string ConnectionString { get; }

    public MsSqlDatabase(string connectionString) => ConnectionString = connectionString;

    public IDbConnection GetConnection() => new SqlConnection(ConnectionString);

    public IDbCommand GetCommand(string query, IDbConnection connection) =>
        new SqlCommand(query, (SqlConnection)connection);

    public IDbDataAdapter GetAdapter(IDbCommand command) => new SqlDataAdapter((SqlCommand)command);

    public DataSet ExecuteQuery(string query)
    {
        var dataSet = new DataSet();

        using var connection = GetConnection();
        connection.Open();
        
        using var command = GetCommand(query, connection);
        
        var adapter = GetAdapter(command);
        adapter.Fill(dataSet);

        return dataSet;
    }
}