using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ApiExercise.Api.Controllers;
using ApiExercise.Application.Users.CreateUser;
using ApiExercise.Application.Users.DeleteUserById;
using ApiExercise.Application.Users.GetUsers;
using ApiExercise.Domain.Users;
using FluentAssertions;
using FunctionalTests.Attributes;
using FunctionalTests.Fixtures;
using FunctionalTests.Given;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace FunctionalTests.UseCases
{
    [Collection("server")]
    public class UsersControllerShould : IClassFixture<HostFixture>
    {
        private readonly HostFixture _host;
        private GivenUser Given { get; }

        public UsersControllerShould(HostFixture host)
        {
            _host = host;
            Given = new GivenUser(host);
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

        [Fact]
        [ResetDatabase]
        public async Task When_an_authenticated_user_is_creating_an_user_should_return_Ok()
        {
            var request = new CreateUserRequest()
            {
                Birthdate = DateTime.Now.AddYears(-20),
                Email = "email@email.com",
                Name = "Kenny",
                GenderId = Gender.Male.Id
            };

            var response = await _host
                .Server
                .CreateHttpApiRequest<UsersController>(c => c.CreateUser(request))
                .WithIdentity(Identities.User)
                .PostAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        [ResetDatabase]
        public async Task When_an_authenticated_user_is_deleting_an_user_should_return_Ok()
        {
            var userGiven = Given.Any_user();
            var request = new DeleteUserByIdRequest
            {
                Id = userGiven.Id
            };

            var response = await _host
                .Server
                .CreateHttpApiRequest<UsersController>(c => c.DeleteUser(request))
                .SendAsync(HttpMethod.Delete.ToString());

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}