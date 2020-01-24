using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ApiExercise.Application.People.Queries;
using ApiExercise.Domain.People;
using ApiExercise.Infrastructure.Context;
using ApiExercise.Infrastructure.Queries.Shared;
using Microsoft.Data.SqlClient;

namespace ApiExercise.Infrastructure.Queries.People
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
