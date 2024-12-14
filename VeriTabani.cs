using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region IDataBase Interface

// Interface tanımlaması. Her database için bir connection sağlıyacak.
public interface IDatabaseConnection
{
    IDbConnection GetConnection();
}
#endregion

#region SQLServerDataBase Kontrol
/// <summary>
/// // SQL Server bağlanmak için sınıf.
/// </summary>
class SqlServerDatabaseConnection : IDatabaseConnection
{
    private readonly string connectionString;
    public SqlServerDatabaseConnection()
    {
        //this.connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
    }

    public IDbConnection GetConnection()
    {
        return new SqlConnection(this.connectionString);
    }
}

#endregion

#region MySqlDataBase Kontrol
/// <summary>
/// // MySQL bağlanmak için sınıf.
/// </summary>
public class MySqlDatabaseConnection : IDatabaseConnection
{
    // Appconfig'ten gelen connectionstringi saklayan field.
    private readonly string connectionString;

    public MySqlDatabaseConnection()
    {
        this.connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
    }

    public IDbConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }
}
#endregion
