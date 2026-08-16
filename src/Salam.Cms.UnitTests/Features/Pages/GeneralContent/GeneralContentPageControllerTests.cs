namespace Salam.Cms.UnitTests.Features.Pages.GeneralContent;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.GeneralContent.Controllers;
using Salam.Cms.Web.Features.GeneralContent.Models;
using Salam.Cms.Web.Features.GeneralContent.ViewModels;

[TestFixture]
public class GeneralContentPageControllerTests
{
    private Mock<GeneralContentPage> _mockGeneralContentPage;

    private GeneralContentPageController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockGeneralContentPage = new Mock<GeneralContentPage>(MockBehavior.Loose);

        _controller = new GeneralContentPageController();
    }

    [Test]
    public void Index_ReturnsAViewModelContainingTheContent()
    {
        // Act
        var result = _controller.Index(_mockGeneralContentPage.Object) as ViewResult;
        var model = result?.Model as GeneralContentPageViewModel;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentPage, Is.EqualTo(_mockGeneralContentPage.Object));
        });
    }

    [TearDown]
    public void TearDown()
    {
        _controller.Dispose();
        _controller = null;
    }
}
