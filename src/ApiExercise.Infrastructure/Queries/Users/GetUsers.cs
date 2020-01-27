using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Shared;
using ApiExercise.Application.Users;
using ApiExercise.Application.Users.Queries;
using ApiExercise.Application.Users.ResponseModels;
using ApiExercise.Domain.Users;
using ApiExercise.Infrastructure.Context;
using ApiExercise.Infrastructure.Extensions;
using ApiExercise.Infrastructure.Queries.Shared;
using ApiExercise.Infrastructure.Shared.Queries;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiExercise.Infrastructure.Queries.Users
{
    public class GetUsers : DapperQueryBase, IQuery, IGetUsers
    {
        public GetUsers(SqlConnection sqlConnection) : base(sqlConnection)
        { }

        private readonly Dictionary<string, string> _columns = new Dictionary<string, string>
        {
            { "id", $"U.[{nameof(User.Id)}]" },
            { "email", $"U.[{nameof(User.Email)}]" },
            { "name", $"U.[{nameof(User.Name)}]" },
            { "birthdate", $"U.[{nameof(User.Birthdate)}]" },
            { "genderId", $"U.[{nameof(User.GenderId)}]" },
            { "genderName", $"G.[{nameof(Gender.Name)}]" }
        };

        public async Task<PaginatedResponse<UserListItemResponseModel>> Query(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, object>();
            var orderBy = new SqlOrderByBuilder($"U.[{nameof(User.Name)}] ASC", _columns, request).Build();
            var pagination = new SqlPaginationBuilder(request).Build();
            var gendersLeftJoin = $"LEFT JOIN [{nameof(ExerciseContext.Genders)}] G ON G.[{nameof(Gender.Id)}] = U.[{nameof(User.GenderId)}]";

            var select = $@"
            SELECT
            U.[{nameof(User.Id)}] AS [{nameof(UserListItemResponseModel.Id)}],
            U.[{nameof(User.Email)}] AS [{nameof(UserListItemResponseModel.Email)}],
            U.[{nameof(User.Name)}] AS [{nameof(UserListItemResponseModel.Name)}],
            U.[{nameof(User.Birthdate)}] AS [{nameof(UserListItemResponseModel.Birthdate)}],
            U.[{nameof(User.GenderId)}] AS [{nameof(UserListItemResponseModel.GenderId)}],
            G.[{nameof(Gender.Name)}] AS [{nameof(UserListItemResponseModel.GenderName)}]
            FROM [{nameof(ExerciseContext.Users)}] U
            {gendersLeftJoin}
            {orderBy} {pagination.Sql}";

            var count = $@"
            SELECT COUNT(U.[{nameof(User.Id)}])
            FROM [{nameof(ExerciseContext.Users)}] U
            {gendersLeftJoin}";

            parameters.AddRange(pagination.Parameters);

            var result = await WithConnection(async connection =>
            {
                var queryResult = await connection.QueryMultipleAsync($"{select};{count}", parameters);
                return new PaginatedResponse<UserListItemResponseModel>(queryResult.Read<UserListItemResponseModel>(), queryResult.ReadFirst<long>());
            }, cancellationToken);
            return result;
        }
    }
}
