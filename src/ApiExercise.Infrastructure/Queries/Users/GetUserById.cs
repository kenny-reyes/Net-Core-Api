using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Users.Queries;
using ApiExercise.Application.Users.ResponseModels;
using ApiExercise.Domain.Users;
using ApiExercise.Infrastructure.Context;
using ApiExercise.Infrastructure.Queries.Shared;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiExercise.Infrastructure.Queries.Users
{
    public class GetUserById : DapperQueryBase, IQuery, IGetUserById
    {
        public GetUserById(SqlConnection sqlConnection) : base(sqlConnection)
        { }

        public async Task<UserModel> Query(int id, CancellationToken cancellationToken)
        {
            var select = $@"
            SELECT
            U.[{nameof(User.Id)}] AS [{nameof(UserModel.Id)}],
            U.[{nameof(User.Email)}] AS [{nameof(UserModel.Email)}],
            U.[{nameof(User.Name)}] AS [{nameof(UserModel.Name)}], 
            U.[{nameof(User.Birthdate)}] AS [{nameof(UserModel.Birthdate)}],
            U.[{nameof(User.GenderId)}] AS [{nameof(UserModel.GenderId)}],
            R.[{nameof(Gender.Name)}] AS [{nameof(UserModel.Gender)}]
            FROM [{nameof(ExerciseContext.Users)}] U
            LEFT JOIN [{nameof(ExerciseContext.Genders)}] R
            ON R.[{nameof(Gender.Id)}] = U.[{nameof(User.GenderId)}]
            WHERE  U.[{nameof(User.Id)}] = @{nameof(id)}";

            var userResult = await WithConnection(async connection =>
            {
                var queryResult = await connection.QueryMultipleAsync(
                    $"{select}",
                    new { id });

                var user = queryResult.ReadFirst<UserModel>();

                return user;
            }, cancellationToken);

            return userResult;
        }
    }
}
