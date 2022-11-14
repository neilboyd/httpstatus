namespace Teapot.Web.Tests.IntegrationTests
{
    [Category("Integration")]
    public class IntegrationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCaseSource(typeof(ExtendedHttpStatusCodes), nameof(ExtendedHttpStatusCodes.StatusCodes))]
        public void Test1([Values] ExtendedHttpStatusCode httpStatusCode)
        {
            var appName = Environment.GetEnvironmentVariable("AZURE_WEBAPP_NAME");
            Assert.That(appName, Is.EqualTo("httpstatus-staging"));
        }
    }
}