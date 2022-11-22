﻿using Teapot.Web.Models;
using Teapot.Web.Models.Unofficial;

namespace Teapot.Web.Tests.UnitTests;

public class TeapotStatusCodeResultsTests
{
    private TeapotStatusCodeResults _target;

    [SetUp]
    public void Setup()
    {
        _target = new TeapotStatusCodeResults(
            new AmazonStatusCodeResults(),
            new CloudflareStatusCodeResults(),
            new EsriStatusCodeResults(),
            new LaravelStatusCodeResults(),
            new MicrosoftStatusCodeResults(),
            new NginxStatusCodeResults(),
            new TwitterStatusCodeResults()
            );
    }

    [Test]
    public void ContainsStandardCode([Values] HttpStatusCode httpStatusCode)
    {
        Assert.That(_target.ContainsKey((int)httpStatusCode));
    }

    [Test]
    [Ignore("do we care that some are different?")]
    public void ReasonIsSameForStandardCode([Values] HttpStatusCode httpStatusCode)
    {
        Assert.That(_target[(int)httpStatusCode].Description, Is.EqualTo(httpStatusCode.ToString()).OnlyAlphanumericIgnoreCase());
    }
}
