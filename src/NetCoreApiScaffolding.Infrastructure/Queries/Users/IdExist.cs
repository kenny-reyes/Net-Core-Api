using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Domain.Users;
using NetCoreApiScaffolding.Infrastructure.Context;
using Dapper;
using Microsoft.Data.SqlClient;
using NetCoreApiScaffolding.Application.Interfaces;
using NetCoreApiScaffolding.Application.Interfaces.Queries;
using NetCoreApiScaffolding.Infrastructure.Common.Queries;

namespace NetCoreApiScaffolding.Infrastructure.Queries.Users
{
    public class IdExists : DapperQueryBase, IQuery, IIdExists
    {
        public IdExists(SqlConnection sqlConnection) : base(sqlConnection)
        {
        }

        public async Task<bool> Query(int id, CancellationToken cancellationToken)
        {
            var select = $@"
            SELECT COUNT(U.[{nameof(User.Id)}])
            FROM [{nameof(DataBaseContext.Users)}] U
            WHERE U.[{nameof(User.Id)}] = @{nameof(id)}";

            var result = await WithConnection(async connection =>
            {
                var existsCount = await connection.QueryFirstAsync<int>(select, new {id});
                return existsCount;
            }, cancellationToken);

            return result != 0;
        }
    }
}