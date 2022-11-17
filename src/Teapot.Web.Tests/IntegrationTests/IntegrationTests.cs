namespace Teapot.Web.Tests.IntegrationTests
{
    [Category("Integration")]
    public class IntegrationTests
    {
        private static readonly HttpClient _httpClient = new();
        private Uri _uri;

        [SetUp]
        public void Setup()
        {
            var appName = Environment.GetEnvironmentVariable("AZURE_WEBAPP_NAME");
            Assert.That(appName, Is.Not.Empty);
            _uri = new Uri($"https://{appName}.azurewebsites.net");
        }

        [TestCaseSource(typeof(ExtendedHttpStatusCodes), nameof(ExtendedHttpStatusCodes.StatusCodes))]
        public async Task Response_Is_Correct([Values] ExtendedHttpStatusCode httpStatusCode)
        {
            var uri = new Uri(_uri, $"/{httpStatusCode.Code}");
            using var response = await _httpClient.GetAsync(uri);
            Assert.That((int)response.StatusCode, Is.EqualTo(httpStatusCode.Code));
            var body = await response.Content.ReadAsStringAsync();
            Assert.That(body, Is.EqualTo(httpStatusCode.ToString()));
        }
    }
}