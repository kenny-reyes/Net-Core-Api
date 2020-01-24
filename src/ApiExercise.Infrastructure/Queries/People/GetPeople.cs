using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ApiExercise.Application.People;
using ApiExercise.Application.People.Queries;
using ApiExercise.Application.People.Responses;
using ApiExercise.Application.Shared;
using ApiExercise.Domain.People;
using ApiExercise.Infrastructure.Context;
using ApiExercise.Infrastructure.Queries.Shared;
using ApiExercise.Infrastructure.Shared.Queries;
using ApiExercise.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;

namespace ApiExercise.Infrastructure.Queries.People
{
    public class GetPeople : DapperQueryBase, IQuery, IGetPeople
    {
        public GetPeople(SqlConnection sqlConnection) : base(sqlConnection)
        { }

        private readonly Dictionary<string, string> _columns = new Dictionary<string, string>
        {
            { "id", $"U.[{nameof(Person.Id)}]" },
            { "email", $"U.[{nameof(Person.Email)}]" },
            { "name", $"U.[{nameof(Person.Name)}]" },
            { "surname", $"U.[{nameof(Person.Surname)}]" },
            { "genderId", $"U.[{nameof(Person.GenderId)}]" },
            { "genderName", $"R.[{nameof(Gender.Name)}]" }
        };

        public async Task<PaginatedResponse<PersonListItemModel>> Query(GetPeopleDataQueryRequest request, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, object>();
            var where = new SqlWhereBuilder(request, _columns).Build();
            var orderBy = new SqlOrderByBuilder($"U.[{nameof(Person.Name)}] ASC", _columns, request).Build();
            var pagination = new SqlPaginationBuilder(request).Build();
            var gendersLeftjoin = $"LEFT JOIN [{nameof(MyContext.Genders)}] R ON R.[{nameof(Gender.Id)}] = U.[{nameof(Person.GenderId)}]";

            var select = $@"
            SELECT
            U.[{nameof(Person.Id)}] AS [{nameof(PersonListItemModel.Id)}],
            U.[{nameof(Person.Email)}] AS [{nameof(PersonListItemModel.Email)}],
            U.[{nameof(Person.Name)}] AS [{nameof(PersonListItemModel.Name)}],
            U.[{nameof(Person.Surname)}] AS [{nameof(PersonListItemModel.Surname)}],
            U.[{nameof(Person.GenderId)}] AS [{nameof(PersonListItemModel.GenderId)}],
            R.[{nameof(Gender.Name)}] AS [{nameof(PersonListItemModel.GenderName)}]
            FROM [{nameof(MyContext.People)}] U
            {gendersLeftjoin}
            {where.Sql} {orderBy} {pagination.Sql}";

            var count = $@"
            SELECT COUNT(U.[{nameof(Person.Id)}])
            FROM [{nameof(MyContext.People)}] U
            {gendersLeftjoin}
            {where.Sql}";

            parameters.AddRange(where.Parameters);
            parameters.AddRange(pagination.Parameters);

            var result = await WithConnection(async connection =>
            {
                var queryResult = await connection.QueryMultipleAsync($"{select};{count}", parameters);
                return new PaginatedResponse<PersonListItemModel>(queryResult.Read<PersonListItemModel>(), queryResult.ReadFirst<long>());
            }, cancellationToken);
            return result;
        }
    }
}
