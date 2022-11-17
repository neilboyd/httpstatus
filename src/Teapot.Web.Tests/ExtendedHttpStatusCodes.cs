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

    public static IEnumerable<ExtendedHttpStatusCode> AllStatusCodes =>
        Overrides
        .Union(CloudflareStatusCodes)
        .Union(Enum.GetValues<HttpStatusCode>()
                   .Select(x => new ExtendedHttpStatusCode((int)x, x.ToString())));

    public static IEnumerable<ExtendedHttpStatusCode> StatusCodesWithContent =>
        Overrides
        .Union(CloudflareStatusCodes)
        .Union(Enum.GetValues<HttpStatusCode>()
                   .Where(x => !x.IsOneOf(Continue, SwitchingProtocols, Processing, EarlyHints, NoContent, ResetContent, MultiStatus, NotModified))
                   .Select(x => new ExtendedHttpStatusCode((int)x, x.ToString())));

    public static IEnumerable<ExtendedHttpStatusCode> StatusCodesNoContent =>
        new[] { SwitchingProtocols, NoContent, ResetContent, NotModified }
            .Select(x => new ExtendedHttpStatusCode((int)x, x.ToString()));

    public static IEnumerable<ExtendedHttpStatusCode> StatusCodesServerError =>
        new[] { Continue, Processing, EarlyHints }
            .Select(x => new ExtendedHttpStatusCode((int)x, x.ToString()));
}
