using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

namespace Laboratorio.CRUD.Company.Infra.Data.DBClient
{
    public class SqlServerConnection
    {
        private readonly string _stringConnection;
        public SqlServerConnection(string stringConnection) { _stringConnection = stringConnection ?? throw new ArgumentNullException(nameof(stringConnection)); }

        public SqlConnection CreateConnection()
        {

            var sqlConnection = new SqlConnection(_stringConnection);
            try
            {
                sqlConnection.Open();
            }
            catch (Exception exception)
            {
                throw new Exception("An error occured while connecting to the database.", exception);
            }
            return sqlConnection;
        }
    }
}
