using static System.Net.HttpStatusCode;

namespace Teapot.Web.Tests;

public class ExtendedHttpStatusCodes
{
    private static readonly ExtendedHttpStatusCode[] Overrides = new[] {
            new ExtendedHttpStatusCode(300, "Multiple Choices"),
            new ExtendedHttpStatusCode(301, "Moved Permanently"),
            new ExtendedHttpStatusCode(302, "Found"),
            new ExtendedHttpStatusCode(303, "See Other"),
            new ExtendedHttpStatusCode(306, "Switch Proxy"),
            new ExtendedHttpStatusCode(418, "I'm a teapot"),
            new ExtendedHttpStatusCode(425, "Too Early")
        };

    private static readonly ExtendedHttpStatusCode[] CloudflareStatusCodes = new[] {
            new ExtendedHttpStatusCode(520, "Web Server Returned an Unknown Error"),
            new ExtendedHttpStatusCode(521, "Web Server Is Down"),
            new ExtendedHttpStatusCode(522, "Connection Timed out"),
            new ExtendedHttpStatusCode(523, "Origin Is Unreachable"),
            new ExtendedHttpStatusCode(524, "A Timeout Occurred"),
            new ExtendedHttpStatusCode(525, "SSL Handshake Failed"),
            new ExtendedHttpStatusCode(526, "Invalid SSL Certificate"),
            new ExtendedHttpStatusCode(527, "Railgun Error"),
            new ExtendedHttpStatusCode(530, "")
        };

    private static readonly HttpStatusCode[] AllStatusCodes = Enum.GetValues<HttpStatusCode>();

    private static readonly HttpStatusCode[] NoContentStatusCodes = new[]
    {
        SwitchingProtocols, NoContent, ResetContent, NotModified
    };

    private static readonly HttpStatusCode[] DifferentContentStatusCodes = new[]
    {
        MultiStatus
    };

    private static readonly HttpStatusCode[] ServerErrorStatusCodes = new[]
    {
        Continue, Processing, EarlyHints
    };

    public static IEnumerable<ExtendedHttpStatusCode> StatusCodesAll =>
        Overrides
        .Union(CloudflareStatusCodes)
        .Union(AllStatusCodes.Select(Map));

    public static IEnumerable<ExtendedHttpStatusCode> StatusCodesWithContent =>
        Overrides
        .Union(CloudflareStatusCodes)
        .Union(AllStatusCodes
                   .Except(NoContentStatusCodes)
                   .Except(ServerErrorStatusCodes)
                   .Except(DifferentContentStatusCodes)
                   .Select(Map));

    public static IEnumerable<ExtendedHttpStatusCode> StatusCodesNoContent =>
        NoContentStatusCodes.Select(Map);

    public static IEnumerable<ExtendedHttpStatusCode> StatusCodesServerError =>
        ServerErrorStatusCodes.Select(Map);

    private static ExtendedHttpStatusCode Map(HttpStatusCode httpStatusCode)
        => new((int)httpStatusCode, httpStatusCode.ToString());
}
