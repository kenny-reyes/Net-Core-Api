using System.Reflection;
using FunctionalTests.Fixtures;
using Xunit.Sdk;

namespace FunctionalTests.Attributes
{
    public class ResetDatabaseAttribute : BeforeAfterTestAttribute
    {
        public override void Before(MethodInfo methodUnderTest) => HostFixture.ResetCheckpoint();
    }
}