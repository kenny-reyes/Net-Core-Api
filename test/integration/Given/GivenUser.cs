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
    }
}