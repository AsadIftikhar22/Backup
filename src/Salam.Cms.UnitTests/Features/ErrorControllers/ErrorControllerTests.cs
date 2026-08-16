namespace Salam.Cms.UnitTests.Features.ErrorControllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Salam.Cms.Web.Features.NotFound.Controllers;
using Salam.Cms.Web.Features.NotFound.Models;
using Salam.Cms.Web.Features.NotFound.ViewModels;
using Stott.Security.Optimizely.Common.Validation;

[TestFixture]
public class ErrorControllerTests
{
    private Mock<INotFoundViewModelBuilder> _mockViewModelBuilder;
    private Mock<ILogger<ErrorController>> _mockLogger;
    private ErrorController _controller;
    private Mock<NotFoundPage> _mockNotFoundPage;

    [SetUp]
    public void SetUp()
    {
        _mockViewModelBuilder = new Mock<INotFoundViewModelBuilder>();
        _mockLogger = new Mock<ILogger<ErrorController>>();
        _mockNotFoundPage = new Mock<NotFoundPage>(MockBehavior.Loose);
        _controller = new ErrorController(_mockViewModelBuilder.Object, _mockLogger.Object);
    }

    [Test]
    public void PageNotFound_ModelStateInvalid_ThrowsInvalidOperationException()
    {
        _controller.ModelState.AddModelError("Key", "Error message");

        var exception = Assert.Throws<InvalidOperationException>(() => _controller.PageNotFound(404));
        Assert.That(exception?.Message, Is.EqualTo(JsonConvert.SerializeObject(new ValidationModel(_controller.ModelState))));
    }

    [Test]
    public void PageNotFound_ModelStateValid_ReturnsViewResult()
    {
        var statusCode = 404;
        var expectedModel = new NotFoundViewModel(_mockNotFoundPage.Object);
        _mockViewModelBuilder.Setup(builder => builder.WithStatusCode(statusCode)).Returns(_mockViewModelBuilder.Object);
        _mockViewModelBuilder.Setup(builder => builder.Build()).Returns(expectedModel);

        var result = _controller.PageNotFound(statusCode) as ViewResult;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(expectedModel));
        });
    }

    [TearDown]
    public void TearDown()
    {
        _controller.Dispose();
        _controller = null;
    }
}
