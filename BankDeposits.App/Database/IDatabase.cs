using System.Data;

namespace BankDeposits.App.Database;

public interface IDatabase
{
    IDbConnection GetConnection();
    
    IDbCommand GetCommand(string query, IDbConnection connection);

    IDbDataAdapter GetAdapter(IDbCommand command);
    
    DataSet ExecuteQuery(string query);
}