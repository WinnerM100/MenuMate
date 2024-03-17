using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace MenuMate.Utilities.Sql;

public class SqlConnector
{
    public SqlConnection DbConnection{get; private set;}

    public SqlConnector(IConfiguration configuration)
    {
        string connString = configuration.GetSection("ConnectionStrings").GetSection("MenuMateDB").Get<string>();
        DbConnection = new SqlConnection(connString);
    }
}