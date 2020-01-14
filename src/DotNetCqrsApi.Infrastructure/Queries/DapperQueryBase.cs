using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCqrsApi.Infrastructure.Queries
{
    public class DapperQueryBase
    {
        private readonly SqlConnection sqlConnection;

        public DapperQueryBase(SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData, CancellationToken cancellationToken)
        {
            try
            {
                await sqlConnection.OpenAsync(cancellationToken);
                return await getData(sqlConnection);
            }
            catch (TimeoutException exception)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", exception);
            }
            catch (SqlException exception)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception (not a timeout)", exception);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}

