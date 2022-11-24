using Microsoft.AspNetCore.Mvc.Testing;

namespace Teapot.Web.Tests.IntegrationTests;

[Category("Integration")]
public class IntegrationTests
{
    private static HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();
    }

    [TestCaseSource(typeof(ExtendedHttpStatusCodes), nameof(ExtendedHttpStatusCodes.StatusCodesWithContent))]
    public async Task ResponseWithContent([Values] ExtendedHttpStatusCode httpStatusCode)
    {
        var uri = $"/{httpStatusCode.Code}";
        using var response = await _httpClient.GetAsync(uri);
        Assert.That((int)response.StatusCode, Is.EqualTo(httpStatusCode.Code));
        var body = await response.Content.ReadAsStringAsync();
        Assert.That(body.ReplaceLineEndings(), Is.EqualTo(httpStatusCode.Body));
    }

    [TestCaseSource(typeof(ExtendedHttpStatusCodes), nameof(ExtendedHttpStatusCodes.StatusCodesNoContent))]
    public async Task ResponseNoContent([Values] ExtendedHttpStatusCode httpStatusCode)
    {
        var uri = $"/{httpStatusCode.Code}";
        using var response = await _httpClient.GetAsync(uri);
        Assert.That((int)response.StatusCode, Is.EqualTo(httpStatusCode.Code));
        var body = await response.Content.ReadAsStringAsync();
        Assert.That(body, Is.Empty);
    }

    [TestCaseSource(typeof(ExtendedHttpStatusCodes), nameof(ExtendedHttpStatusCodes.StatusCodesServerError))]
    public async Task ResponseServerError([Values] ExtendedHttpStatusCode httpStatusCode)
    {
        var uri = $"/{httpStatusCode.Code}";
        using var response = await _httpClient.GetAsync(uri);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadGateway));
        var body = await response.Content.ReadAsStringAsync();
        Assert.That(body, Is.Not.Empty);
    }
}
