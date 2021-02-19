using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Common.Queries;
using NetCoreApiScaffolding.Application.Common.ResponseModels;
using NetCoreApiScaffolding.Domain.Users;
using NetCoreApiScaffolding.Infrastructure.Context;
using NetCoreApiScaffolding.Infrastructure.Queries.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;

namespace NetCoreApiScaffolding.Infrastructure.Queries.Users
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
            G.[{nameof(Gender.Name)}] AS [{nameof(UserResponseModel.Gender)}]
            FROM [{nameof(DataBaseContext.Users)}] U
            LEFT JOIN [{nameof(DataBaseContext.Genders)}] G
            ON G.[{nameof(Gender.Id)}] = U.[{nameof(User.GenderId)}]
            WHERE  U.[{nameof(User.Id)}] = @{nameof(id)}";

            var result = await WithConnection(async connection =>
                await connection.QueryFirstOrDefaultAsync<UserResponseModel>(select, new { id }), cancellationToken);

            return result;
        }
    }
}
