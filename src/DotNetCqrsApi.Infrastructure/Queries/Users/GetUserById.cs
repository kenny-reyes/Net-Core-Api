using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DotNetCqrsApi.Application.Person.Queries;
using DotNetCqrsApi.Infrastructure.Context;
using DotNetCqrsApi.Infrastructure.Queries.Shared;

namespace DotNetCqrsApi.Infrastructure.Queries.Users
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
            U.[{nameof(User.Surname)}] AS [{nameof(UserModel.Surname)}],
            U.[{nameof(User.RoleId)}] AS [{nameof(UserModel.RoleId)}],
            R.[{nameof(Role.Name)}] AS [{nameof(UserModel.RoleName)}],
            U.[{nameof(User.IsEnabled)}] AS [{nameof(UserModel.IsEnabled)}]
            FROM [{nameof(MyContext.Users)}] U
            LEFT JOIN [{nameof(MyContext.Roles)}] R
            ON R.[{nameof(Role.Id)}] = U.[{nameof(User.RoleId)}]
            WHERE  U.[{nameof(User.Id)}] = @{nameof(id)}";

            var selectCountries = $@"
            SELECT
            C.[{nameof(Country.Id)}] AS [{nameof(CountryModel.Id)}],
            C.[{nameof(Country.Code)}] AS [{nameof(CountryModel.Code)}],
            C.[{nameof(Country.Name)}] AS [{nameof(CountryModel.Name)}],
            C.[{nameof(Country.CurrencyId)}] AS [{nameof(CountryModel.CurrencyId)}]
            FROM [{nameof(MyContext.Countries)}] C
            LEFT JOIN [{nameof(MyContext.UserCountries)}] UC
            ON UC.[{nameof(UserCountry.CountryId)}] = C.[{nameof(Country.Id)}]
            WHERE UC.[{nameof(UserCountry.UserId)}] = @{nameof(id)}";

            var selectSections = $@"
            SELECT
            C.[{nameof(BitVector32.Section.Id)}] AS [{nameof(SectionModel.Id)}],
            C.[{nameof(BitVector32.Section.Name)}] AS [{nameof(SectionModel.Name)}]
            FROM [{nameof(MyContext.Sections)}] C
            LEFT JOIN [{nameof(MyContext.UserSections)}] US
            ON US.[{nameof(UserSection.SectionId)}] = C.[{nameof(BitVector32.Section.Id)}]
            WHERE US.[{nameof(UserSection.UserId)}] = @{nameof(id)}";

            var selectModules = $@"
            SELECT
            M.[{nameof(Module.Id)}] AS [{nameof(ModuleModel.Id)}],
            M.[{nameof(Module.Description)}] AS [{nameof(ModuleModel.Description)}],
            M.[{nameof(Module.CountryId)}] AS [{nameof(ModuleModel.CountryId)}]
            FROM [{nameof(MyContext.Modules)}] M
            LEFT JOIN [{nameof(MyContext.UserModules)}] UM
            ON UM.[{nameof(UserModule.ModuleId)}] = M.[{nameof(Module.Id)}]
            WHERE UM.[{nameof(UserModule.UserId)}] = @{nameof(id)} AND
            M.[{nameof(Module.CountryId)}] IN @countryIds";

            IEnumerable<int> countryIds = new List<int>();

            var userResult = await WithConnection(async connection =>
            {
                var queryResult = await connection.QueryMultipleAsync(
                    $"{select};{selectCountries};{selectSections}",
                    new { id });

                var user = queryResult.ReadFirst<UserModel>();
                user.Countries = queryResult.Read<CountryModel>();
                countryIds = user.Countries.Select(c => c.Id);
                user.Sections = queryResult.Read<SectionModel>();

                return user;
            }, cancellationToken);

            var modulesResult = await WithConnection(async connection =>
            {
                var modules = await connection.QueryAsync<ModuleModel>(selectModules, new { id, countryIds });
                foreach (var country in userResult.Countries)
                {
                    country.Modules = modules.Where(m => m.CountryId == country.Id);
                }

                return modules;
            }, cancellationToken);

            return userResult;
        }
    }
}
