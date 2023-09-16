using System.Data;

namespace BankDeposits.App.Database;

public class AbstractDatabase<TConnection, TCommand, TDataAdapter> : IDatabase
    where TConnection : IDbConnection, new()
    where TCommand : IDbCommand, new()
    where TDataAdapter : IDbDataAdapter, new()
{
    private string ConnectionString { get; }

    protected AbstractDatabase(string connectionString) => ConnectionString = connectionString;

    public IDbConnection GetConnection() => new TConnection { ConnectionString = ConnectionString };

    public IDbCommand GetCommand(string query, IDbConnection connection) =>
        new TCommand { CommandText = query, Connection = connection };

    public IDbDataAdapter GetAdapter(IDbCommand command) => new TDataAdapter { SelectCommand = (TCommand)command };

    public DataSet ExecuteQuery(string query)
    {
        using var connection = GetConnection();
        connection.Open();

        using var command = GetCommand(query, connection);

        var adapter = GetAdapter(command);

        var dataSet = new DataSet();
        adapter.Fill(dataSet);

        return dataSet;
    }
}