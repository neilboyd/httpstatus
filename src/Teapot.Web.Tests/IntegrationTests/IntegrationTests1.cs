namespace Teapot.Web.Tests.IntegrationTests
{
    [Category("Integration")]
    public class IntegrationTests
    {
        private Uri _uri;

        [SetUp]
        public void Setup()
        {
            var appName = Environment.GetEnvironmentVariable("AZURE_WEBAPP_NAME");
            Assert.That(appName, Is.Not.Empty);
            _uri = new Uri($"https://{appName}.azurewebsites.net");
        }

        [TestCaseSource(typeof(ExtendedHttpStatusCodes), nameof(ExtendedHttpStatusCodes.StatusCodes))]
        public void Test1([Values] ExtendedHttpStatusCode httpStatusCode)
        {
            var appName = Environment.GetEnvironmentVariable("AZURE_WEBAPP_NAME");
            Assert.That(appName, Is.EqualTo("httpstatus-staging"));
        }
    }
}