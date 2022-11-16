using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Teapot.Web.Models;

namespace Teapot.Web.Tests.UnitTests
{
    public class CustomHttpStatusCodeResultTests
    {
        private Mock<HttpContext> _mockHttpContext;
        private Mock<ActionContext> _mockActionContext;

        [SetUp]
        public void Setup()
        {
            var logger = new Mock<ILogger>();
            var loggerFactory = new Mock<ILoggerFactory>();
            var serviceProvider = new Mock<IServiceProvider>();
            loggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(logger.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILogger))).Returns(logger.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILoggerFactory))).Returns(loggerFactory.Object);

            var headerDictionary = new HeaderDictionary();
            var httpRequest = new Mock<HttpRequest>();
            var httpResponse = new Mock<HttpResponse>();
            var features = new Mock<IFeatureCollection>();
            httpRequest.SetupGet(x => x.Headers).Returns(headerDictionary);

            _mockHttpContext = new Mock<HttpContext>();
            _mockHttpContext.Setup(x => x.Features).Returns(features.Object);
            _mockHttpContext.Setup(x => x.Request).Returns(httpRequest.Object);
            _mockHttpContext.Setup(x => x.Response).Returns(httpResponse.Object);
            _mockHttpContext.Setup(x => x.RequestServices).Returns(serviceProvider.Object);

            var routeData = new Mock<RouteData>();
            var actionDescriptor = new Mock<ActionDescriptor>();

            _mockActionContext = new Mock<ActionContext>(_mockHttpContext.Object, routeData.Object, actionDescriptor.Object);
        }

        [TestCaseSource(typeof(ExtendedHttpStatusCodes), nameof(ExtendedHttpStatusCodes.StatusCodes))]
        public async Task ResponseStatusCode_IsCorrect(ExtendedHttpStatusCode httpStatusCode)
        {
            var statusCodeResult = new TeapotStatusCodeResult
            {
                Description = httpStatusCode.Message
            };

            var target = new CustomHttpStatusCodeResult(httpStatusCode.Code, statusCodeResult);

            await target.ExecuteResultAsync(_mockActionContext.Object);

            _mockHttpContext.VerifySet(x => x.Response.StatusCode = httpStatusCode.Code);
        }
    }
}