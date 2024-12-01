using System.Data.SqlClient;

namespace ADO.NET_CRUD_MVC.Factory;

public class SqlConnectionFactory(string connectionString)
{
    public SqlConnection Create()
    {
        return new SqlConnection(connectionString);
    }
}
