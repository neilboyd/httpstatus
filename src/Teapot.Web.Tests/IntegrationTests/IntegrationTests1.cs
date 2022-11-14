namespace Teapot.Web.Tests.IntegrationTests
{
    [Category("Integration")]
    public class IntegrationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1([Values] HttpStatusCode httpStatusCode)
        {
            var appName = Environment.GetEnvironmentVariable("AZURE_WEBAPP_NAME");
            Assert.That(appName, Is.EqualTo("httpstatus-staging"));
        }

        // TODO the same test for the unofficial status codes, eg 418 I'm a teapot
    }
}