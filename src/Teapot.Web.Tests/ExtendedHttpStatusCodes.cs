using Teapot.Web.Models;
using Teapot.Web.Models.Unofficial;
using static System.Net.HttpStatusCode;

namespace Teapot.Web.Tests;

public class ExtendedHttpStatusCodes
{
    private static readonly TeapotStatusCodeResults All = new (
            new AmazonStatusCodeResults(),
            new CloudflareStatusCodeResults(),
            new EsriStatusCodeResults(),
            new LaravelStatusCodeResults(),
            new MicrosoftStatusCodeResults(),
            new NginxStatusCodeResults(),
            new TwitterStatusCodeResults()
            );

    private static readonly HttpStatusCode[] NoContentStatusCodes = new[]
    {
        SwitchingProtocols, NoContent, ResetContent, NotModified
    };

    private static readonly HttpStatusCode[] ServerErrorStatusCodes = new[]
    {
        Continue, Processing, EarlyHints
    };

    public static IEnumerable<ExtendedHttpStatusCode> StatusCodesAll =>
        All.Select(x => new ExtendedHttpStatusCode(x.Key, x.Value.Description, x.Value.Body));

    public static IEnumerable<ExtendedHttpStatusCode> StatusCodesWithContent =>
        All
        .Where(x => !x.Value.ExcludeBody)
        .Select(x => new ExtendedHttpStatusCode(x.Key, x.Value.Description, x.Value.Body));

    public static IEnumerable<ExtendedHttpStatusCode> StatusCodesNoContent =>
        NoContentStatusCodes.Select(Map);

    public static IEnumerable<ExtendedHttpStatusCode> StatusCodesServerError =>
        ServerErrorStatusCodes.Select(Map);

    private static ExtendedHttpStatusCode Map(HttpStatusCode httpStatusCode)
        => new((int)httpStatusCode, httpStatusCode.ToString(), null);
}
