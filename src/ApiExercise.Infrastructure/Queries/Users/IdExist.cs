using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Common.Queries;
using ApiExercise.Domain.Users;
using ApiExercise.Infrastructure.Context;
using ApiExercise.Infrastructure.Queries.Shared;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiExercise.Infrastructure.Queries.Users
{
    public class IdExists : DapperQueryBase, IQuery, IIdExists
    {
        public IdExists(SqlConnection sqlConnection) : base(sqlConnection)
        { }
        
        public async Task<bool> Query(int id, CancellationToken cancellationToken)
        {
            var select = $@"
            SELECT COUNT(U.[{nameof(User.Id)}])
            FROM [{nameof(ExerciseContext.Users)}] U
            WHERE U.[{nameof(User.Id)}] = @{nameof(id)}";

            var result = await WithConnection(async connection =>
            {
                var existsCount = await connection.QueryFirstAsync<int>(select, new { id });
                return existsCount;
            }, cancellationToken);

            return result != 0;
        }
    }
}
