using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IUserRepository
{
    bool ValidateUser(string username, string password);
}

public class UserRepository : IUserRepository
{
    private readonly IDatabaseConnection _databaseConnection;

    public UserRepository(IDatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public bool ValidateUser(string username,string password)
    {
        using (IDbConnection connection = _databaseConnection.GetConnection())
        {
            connection.Open();
            string query = "SELECT COUNT(1) FROM users WHERE Username=@Username AND Password = @Password";
            IDbCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.Add(new MySqlParameter("@Username", username));
            command.Parameters.Add(new MySqlParameter("@Password", password));

            int count = Convert.ToInt32(command.ExecuteScalar());
            return count == 1;
        }
    }
}
