namespace Teapot.Web.Tests
{
    public class ExtendedHttpStatusCodes
    {
        public static IEnumerable<ExtendedHttpStatusCode> StatusCodes =>
            new[] {
                new ExtendedHttpStatusCode(300, "Multiple Choices"),
                new ExtendedHttpStatusCode(301, "Moved Permanently"),
                new ExtendedHttpStatusCode(302, "Found"),
                new ExtendedHttpStatusCode(303, "See Other"),
                new ExtendedHttpStatusCode(306, "Switch Proxy"),
                new ExtendedHttpStatusCode(418, "I'm a teapot"),
                new ExtendedHttpStatusCode(424, "424 Unknown Code")
            }
            .Union(Enum.GetValues<HttpStatusCode>()
                       .Select(x => new ExtendedHttpStatusCode((int)x, x.ToString())));
    }
}
