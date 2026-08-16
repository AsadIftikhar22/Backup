namespace Salam.Cms.UnitTests.Features.ErrorControllers;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Shared.Models.Pages.ServerError;
using System.Net;

[TestFixture]
public class ServerErrorControllerTests
{
    private ServerErrorController _controller;

    [SetUp]
    public void SetUp()
    {
        _controller = new ServerErrorController();
        var httpContext = new DefaultHttpContext();
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    [Test]
    public void ServerError_ReturnsServiceUnavailableStatusCode()
    {
        _controller.ServerError();

        Assert.That(_controller.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.ServiceUnavailable));
    }

    [Test]
    public void ServerError_ReturnsViewResult()
    {
        var result = _controller.ServerError() as ViewResult;

        Assert.That(result, Is.Not.Null);
    }

    [TearDown]
    public void TearDown()
    {
        _controller.Dispose();
        _controller = null;
    }
}
