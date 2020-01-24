using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ApiExercise.Application.People.Queries;
using ApiExercise.Application.People.Responses;
using ApiExercise.Domain.People;
using ApiExercise.Infrastructure.Context;
using ApiExercise.Infrastructure.Queries.Shared;
using Microsoft.Data.SqlClient;

namespace ApiExercise.Infrastructure.Queries.People
{
    public class GetPersonById : DapperQueryBase, IQuery, IGetPersonById
    {
        public GetPersonById(SqlConnection sqlConnection) : base(sqlConnection)
        { }

        public async Task<PersonModel> Query(int id, CancellationToken cancellationToken)
        {
            var select = $@"
            SELECT
            U.[{nameof(Person.Id)}] AS [{nameof(PersonModel.Id)}],
            U.[{nameof(Person.Email)}] AS [{nameof(PersonModel.Email)}],
            U.[{nameof(Person.Name)}] AS [{nameof(PersonModel.Name)}], 
            U.[{nameof(Person.Surname)}] AS [{nameof(PersonModel.Surname)}],
            U.[{nameof(Person.GenderId)}] AS [{nameof(PersonModel.GenderId)}],
            R.[{nameof(Gender.Name)}] AS [{nameof(PersonModel.Gender)}]
            FROM [{nameof(MyContext.People)}] U
            LEFT JOIN [{nameof(MyContext.Genders)}] R
            ON R.[{nameof(Gender.Id)}] = U.[{nameof(Person.GenderId)}]
            WHERE  U.[{nameof(Person.Id)}] = @{nameof(id)}";

            var personResult = await WithConnection(async connection =>
            {
                var queryResult = await connection.QueryMultipleAsync(
                    $"{select}",
                    new { id });

                var person = queryResult.ReadFirst<PersonModel>();

                return person;
            }, cancellationToken);

            return personResult;
        }
    }
}
