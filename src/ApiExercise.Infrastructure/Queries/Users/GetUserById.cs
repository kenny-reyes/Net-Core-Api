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

        public async Task<UserResponseModel> Query(int id, CancellationToken cancellationToken)
        {
            var select = $@"
            SELECT
            U.[{nameof(User.Id)}] AS [{nameof(UserResponseModel.Id)}],
            U.[{nameof(User.Email)}] AS [{nameof(UserResponseModel.Email)}],
            U.[{nameof(User.Name)}] AS [{nameof(UserResponseModel.Name)}], 
            U.[{nameof(User.Birthdate)}] AS [{nameof(UserResponseModel.Birthdate)}],
            U.[{nameof(User.GenderId)}] AS [{nameof(UserResponseModel.GenderId)}],
            R.[{nameof(Gender.Name)}] AS [{nameof(UserResponseModel.Gender)}]
            FROM [{nameof(ExerciseContext.Users)}] U
            LEFT JOIN [{nameof(ExerciseContext.Genders)}] R
            ON R.[{nameof(Gender.Id)}] = U.[{nameof(User.GenderId)}]
            WHERE  U.[{nameof(User.Id)}] = @{nameof(id)}";

            var userResult = await WithConnection(async connection =>
            {
                var queryResult = await connection.QueryMultipleAsync(
                    $"{select}",
                    new { id });

                var user = queryResult.ReadFirst<UserResponseModel>();

                return user;
            }, cancellationToken);

            return userResult;
        }
    }
}
