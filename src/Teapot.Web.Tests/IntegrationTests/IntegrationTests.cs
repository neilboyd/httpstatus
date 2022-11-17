namespace Teapot.Web.Tests.IntegrationTests;

[Category("Integration")]
public class IntegrationTests
{
    private static readonly HttpClient _httpClient = new(new HttpClientHandler { AllowAutoRedirect = false });
    private Uri _uri;

    [SetUp]
    public void Setup()
    {
        var appName = Environment.GetEnvironmentVariable("AZURE_WEBAPP_NAME");
        Assert.That(appName, Is.Not.Empty);
        _uri = new Uri($"https://{appName}.azurewebsites.net");
    }

    [TestCaseSource(typeof(ExtendedHttpStatusCodes), nameof(ExtendedHttpStatusCodes.StatusCodesWithContent))]
    public async Task ResponseWithContent_Is_Correct([Values] ExtendedHttpStatusCode httpStatusCode)
    {
        var uri = new Uri(_uri, $"/{httpStatusCode.Code}");
        using var response = await _httpClient.GetAsync(uri);
        Assert.That((int)response.StatusCode, Is.EqualTo(httpStatusCode.Code));
        var body = await response.Content.ReadAsStringAsync();
        Assert.That(body, Is.EqualTo(httpStatusCode.ToString()).OnlyAlphanumericIgnoreCase());
    }

    [TestCaseSource(typeof(ExtendedHttpStatusCodes), nameof(ExtendedHttpStatusCodes.StatusCodesNoContent))]
    public async Task ResponseNoContent_Is_Correct([Values] ExtendedHttpStatusCode httpStatusCode)
    {
        var uri = new Uri(_uri, $"/{httpStatusCode.Code}");
        using var response = await _httpClient.GetAsync(uri);
        Assert.That((int)response.StatusCode, Is.EqualTo(httpStatusCode.Code));
        var body = await response.Content.ReadAsStringAsync();
        Assert.That(body, Is.Empty);
    }

    [Test]
    public async Task ResponseMultiStatus_Is_Correct()
    {
        var uri = new Uri(_uri, $"/{(int)HttpStatusCode.MultiStatus}");
        using var response = await _httpClient.GetAsync(uri);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MultiStatus));
        var body = await response.Content.ReadAsStringAsync();
        Assert.That(body.StartsWith("<?xml"), Is.True);
    }
}
