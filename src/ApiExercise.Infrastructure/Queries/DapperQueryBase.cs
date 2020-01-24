using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ApiExercise.Infrastructure.Queries
{
    public class DapperQueryBase
    {
        private readonly SqlConnection _sqlConnection;

        public DapperQueryBase(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData, CancellationToken cancellationToken)
        {
            try
            {
                await _sqlConnection.OpenAsync(cancellationToken);
                return await getData((IDbConnection)_sqlConnection);
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
                if (_sqlConnection.State == ConnectionState.Open)
                {
                    _sqlConnection.Close();
                }
            }
        }
    }
}

