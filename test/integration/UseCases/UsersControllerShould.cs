using System.Net;
using System.Threading.Tasks;
using ApiExercise.Api.Controllers;
using ApiExercise.Application.Users;
using ApiExercise.Application.Users.GetUsers;
using FluentAssertions;
using FunctionalTests.Attributes;
using FunctionalTests.Fixtures;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using GivenFixture = FunctionalTests.Fixtures.GivenFixture;

namespace FunctionalTests.UseCases
{
    [Collection("server")]
    public class UsersControllerShould : IClassFixture<HostFixture>
    {
        private readonly HostFixture _host;
        private GivenFixture Given { get; }

        public UsersControllerShould(HostFixture host)
        {
            _host = host;
            Given = new GivenFixture(host);
        }

        [Fact]
        [ResetDatabase]
        public async Task When_an_authenticated_user_is_getting_all_users_should_return_Ok()
        {
            var request = new GetUsersRequest
            {
                Skip = 0,
                Take = 10
            };

            var response = await _host
                .Server
                .CreateHttpApiRequest<UsersController>(c => c.GetUsers(request))
                .WithIdentity(Identities.User)
                .PostAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}