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
            new ExtendedHttpStatusCode(424, "424 Unknown Code")
        };

    public static IEnumerable<ExtendedHttpStatusCode> AllStatusCodes =>
        Overrides
        .Union(Enum.GetValues<HttpStatusCode>()
                   .Select(x => new ExtendedHttpStatusCode((int)x, x.ToString())));

    public static IEnumerable<ExtendedHttpStatusCode> StatusCodesWithContent =>
        Overrides
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
