﻿using Microsoft.AspNetCore.Mvc.Testing;

namespace Teapot.Web.Tests.IntegrationTests;

[TestFixtureSource(typeof(HttpMethods), nameof(HttpMethods.All))]
public class IntegrationTests
{
    private readonly HttpMethod _httpMethod;

    private static readonly HttpClient _httpClient = new WebApplicationFactory<Program>().CreateDefaultClient();

    public IntegrationTests(HttpMethod httpMethod)
    {
        _httpMethod = httpMethod;
    }

    [TestCaseSource(typeof(TestCases), nameof(TestCases.StatusCodesWithContent))]
    public async Task ResponseWithContent([Values] TestCase testCase)
    {
        var uri = $"/{testCase.Code}";
        using var httpRequest = new HttpRequestMessage(_httpMethod, uri);
        using var response = await _httpClient.SendAsync(httpRequest);
        Assert.That((int)response.StatusCode, Is.EqualTo(testCase.Code));
        var body = await response.Content.ReadAsStringAsync();
        Assert.That(body.ReplaceLineEndings(), Is.EqualTo(testCase.Body));
    }

    [TestCaseSource(typeof(TestCases), nameof(TestCases.StatusCodesNoContent))]
    public async Task ResponseNoContent([Values] TestCase testCase)
    {
        var uri = $"/{testCase.Code}";
        using var httpRequest = new HttpRequestMessage(_httpMethod, uri);
        using var response = await _httpClient.SendAsync(httpRequest);
        Assert.That((int)response.StatusCode, Is.EqualTo(testCase.Code));
        var body = await response.Content.ReadAsStringAsync();
        Assert.That(body, Is.Empty);
    }
}
