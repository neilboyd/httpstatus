namespace Teapot.Web.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var appName = Environment.GetEnvironmentVariable("AZURE_WEBAPP_NAME");
            Assert.That(appName, Is.EqualTo("httpstatus-staging"));
        }
    }
}