using Microsoft.AspNetCore.Mvc;
using Teapot.Web.Models;

namespace Teapot.Web.Tests.UnitTests
{
    public class CustomHttpStatusCodeResultTests
    {
        [Test]
        public async Task ResponseStatusCode_IsCorrect([Values] HttpStatusCode httpStatusCode)
        {
            var statusCodeResult = new TeapotStatusCodeResult
            {
                Description = httpStatusCode.ToString()
            };
            var mockContext = new Mock<ActionContext>();

            var target = new CustomHttpStatusCodeResult((int)httpStatusCode, statusCodeResult);

            await target.ExecuteResultAsync(mockContext.Object);

            mockContext.VerifySet(x => x.HttpContext.Response.StatusCode = (int)httpStatusCode);
        }

        // TODO the same test for the unofficial status codes, eg 418 I'm a teapot
    }
}