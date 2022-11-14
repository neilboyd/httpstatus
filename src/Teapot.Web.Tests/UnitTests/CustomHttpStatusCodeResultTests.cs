using Microsoft.AspNetCore.Mvc;
using Teapot.Web.Models;

namespace Teapot.Web.Tests.UnitTests
{
    public class CustomHttpStatusCodeResultTests
    {
        [TestCaseSource(typeof(ExtendedHttpStatusCodes), nameof(ExtendedHttpStatusCodes.StatusCodes))]
        public async Task ResponseStatusCode_IsCorrect(ExtendedHttpStatusCode httpStatusCode)
        {
            var statusCodeResult = new TeapotStatusCodeResult
            {
                Description = httpStatusCode.Message
            };
            var mockContext = new Mock<ActionContext>();

            var target = new CustomHttpStatusCodeResult(httpStatusCode.Code, statusCodeResult);

            await target.ExecuteResultAsync(mockContext.Object);

            mockContext.VerifySet(x => x.HttpContext.Response.StatusCode = httpStatusCode.Code);
        }
    }
}