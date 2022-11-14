namespace Teapot.Web.Tests
{
    public class ExtendedHttpStatusCodes
    {
        public static IEnumerable<ExtendedHttpStatusCode> StatusCodes =>
            Enum.GetValues<HttpStatusCode>()
            .Select(x => new ExtendedHttpStatusCode((int)x, x.ToString()))
            .Concat(new[] {
                new ExtendedHttpStatusCode(418, "I'm a teapot") });
        // TODO any others?
    }
}