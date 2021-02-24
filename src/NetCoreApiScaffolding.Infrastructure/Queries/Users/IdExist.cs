﻿using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Common.Queries;
using NetCoreApiScaffolding.Domain.Users;
using NetCoreApiScaffolding.Infrastructure.Context;
using NetCoreApiScaffolding.Infrastructure.Queries.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;

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