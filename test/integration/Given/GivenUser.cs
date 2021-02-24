using System;
using NetCoreApiScaffolding.Domain.Users;
using FunctionalTests.Fixtures;

namespace FunctionalTests.Given
{
    public class GivenUser : GivenFixture
    {
        private readonly HostFixture _host;

        public GivenUser(HostFixture host) : base(host)
        {
            _host = host;
        }

        public User Any_user()
        {
            var random = new Random();
            var user = User.Create(
                "email@email.com",
                "name",
                DateTime.Now.AddYears(-50).AddMonths(random.Next(-480, 540)),
                random.Next(100) <= 50 ? Gender.Male.Id : Gender.Female.Id);
            DbContext.Set<User>().Add(user);
            DbContext.SaveChanges();
            return user;
        }
    }
}