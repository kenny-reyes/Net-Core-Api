using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Common.Queries;
using NetCoreApiScaffolding.Domain.Users;
using NetCoreApiScaffolding.Infrastructure.Context;
using NetCoreApiScaffolding.Infrastructure.Queries.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;

namespace NetCoreApiScaffolding.Infrastructure.Queries.Users
{
    public class EmailAlreadyExistsCount : DapperQueryBase, IQuery, IEmailAlreadyExistsCount
    {
        public EmailAlreadyExistsCount(SqlConnection sqlConnection) : base(sqlConnection)
        { }

        public async Task<int> Query(string email, int id, CancellationToken cancellationToken)
        {
            var select = $@"
            SELECT COUNT(U.[{nameof(User.Id)}])
            FROM [{nameof(DataBaseContext.Users)}] U
            WHERE U.[{nameof(User.Email)}] = @{nameof(email)}";
            select += id > 0 ? $" AND U.[{nameof(User.Id)}] <> @{nameof(id)}" : string.Empty;

            var result = await WithConnection(async connection =>
            {
                var existsCount = await connection.QueryFirstAsync<int>(select, new { email, id });
                return existsCount;
            }, cancellationToken);

            return result;
        }
    }
}
