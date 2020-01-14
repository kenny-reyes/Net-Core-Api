using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DotNetCqrsApi.Application.Person;
using DotNetCqrsApi.Application.Person.Queries;
using DotNetCqrsApi.Application.Shared;
using DotNetCqrsApi.Infrastructure.Context;
using DotNetCqrsApi.Infrastructure.Extensions;
using DotNetCqrsApi.Infrastructure.Queries.Shared;
using DotNetCqrsApi.Infrastructure.Shared.Queries;

namespace DotNetCqrsApi.Infrastructure.Queries.Users
{
    public class GetUsers : DapperQueryBase, IQuery, IGetUsers
    {
        private static readonly string SelectCountUserCountries = $@"
        (SELECT COUNT(UC.[{nameof(UserCountry.UserId)}])
        FROM [{nameof(MyContext.UserCountries)}] UC
        WHERE UC.[{nameof(UserCountry.UserId)}] = U.[{nameof(User.Id)}])";

        private static readonly string SelectCountUserSections = $@"
        (SELECT COUNT(US.[{nameof(UserSection.UserId)}])
        FROM [{nameof(MyContext.UserSections)}] US
        WHERE US.[{nameof(UserSection.UserId)}] = U.[{nameof(User.Id)}])";

        public GetUsers(SqlConnection sqlConnection) : base(sqlConnection)
        { }

        private readonly Dictionary<string, string> _columns = new Dictionary<string, string>
        {
            { "id", $"U.[{nameof(User.Id)}]" },
            { "email", $"U.[{nameof(User.Email)}]" },
            { "name", $"U.[{nameof(User.Name)}]" },
            { "surname", $"U.[{nameof(User.Surname)}]" },
            { "roleId", $"U.[{nameof(User.RoleId)}]" },
            { "roleName", $"R.[{nameof(Role.Name)}]" },
            { "isEnabled", $"U.[{nameof(User.IsEnabled)}]" },
            { "countries", SelectCountUserCountries },
            { "sections", SelectCountUserSections }
        };

        public async Task<PaginatedResponse<UserListItemModel>> Query(GetUsersDataQueryRequest request, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, object>();
            var where = new SqlWhereBuilder(request, _columns).Build();
            var orderBy = new SqlOrderByBuilder($"U.[{nameof(User.Name)}] ASC", _columns, request).Build();
            var pagination = new SqlPaginationBuilder(request).Build();
            var rolesLeftjoin = $"LEFT JOIN [{nameof(MyContext.Roles)}] R ON R.[{nameof(Role.Id)}] = U.[{nameof(User.RoleId)}]";

            var select = $@"
            SELECT
            U.[{nameof(User.Id)}] AS [{nameof(UserListItemModel.Id)}],
            U.[{nameof(User.Email)}] AS [{nameof(UserListItemModel.Email)}],
            U.[{nameof(User.Name)}] AS [{nameof(UserListItemModel.Name)}],
            U.[{nameof(User.Surname)}] AS [{nameof(UserListItemModel.Surname)}],
            U.[{nameof(User.RoleId)}] AS [{nameof(UserListItemModel.RoleId)}],
            R.[{nameof(Role.Name)}] AS [{nameof(UserListItemModel.RoleName)}],
            U.[{nameof(User.IsEnabled)}] AS [{nameof(UserListItemModel.IsEnabled)}],
            {SelectCountUserCountries} AS [{nameof(UserListItemModel.Countries)}],
            {SelectCountUserSections} AS [{nameof(UserListItemModel.Sections)}]
            FROM [{nameof(MyContext.Users)}] U
            {rolesLeftjoin}
            {where.Sql} {orderBy} {pagination.Sql}";

            var count = $@"
            SELECT COUNT(U.[{nameof(User.Id)}])
            FROM [{nameof(MyContext.Users)}] U
            {rolesLeftjoin}
            {where.Sql}";

            parameters.AddRange(where.Parameters);
            parameters.AddRange(pagination.Parameters);

            var result = await WithConnection(async connection =>
            {
                var queryResult = await connection.QueryMultipleAsync($"{select};{count}", parameters);
                return new PaginatedResponse<UserListItemModel>(queryResult.Read<UserListItemModel>(), queryResult.ReadFirst<long>());
            }, cancellationToken);
            return result;
        }
    }
}
