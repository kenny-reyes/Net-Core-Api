﻿using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DotNetCqrsApi.Application.People.Queries;
using DotNetCqrsApi.Domain.People;
using DotNetCqrsApi.Infrastructure.Context;
using DotNetCqrsApi.Infrastructure.Queries.Shared;

namespace DotNetCqrsApi.Infrastructure.Queries.People
{
    public class EmailAlreadyExistsCount : DapperQueryBase, IQuery, IEmailAlreadyExistsCount
    {
        public EmailAlreadyExistsCount(SqlConnection sqlConnection) : base(sqlConnection)
        { }

        public async Task<int> Query(string email, int id, CancellationToken cancellationToken)
        {
            var select = $@"
            SELECT COUNT(U.[{nameof(Person.Id)}])
            FROM [{nameof(MyContext.People)}] U
            WHERE U.[{nameof(Person.Email)}] = @{nameof(email)}";
            select += id > 0 ? $" AND U.[{nameof(Person.Id)}] <> @{nameof(id)}" : string.Empty;

            var result = await WithConnection(async connection =>
            {
                var existsCount = await connection.QueryFirstAsync<int>(select, new { email, id });
                return existsCount;
            }, cancellationToken);

            return result;
        }
    }
}
