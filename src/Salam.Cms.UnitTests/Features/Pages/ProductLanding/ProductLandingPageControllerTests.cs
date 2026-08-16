namespace Salam.Cms.UnitTests.Features.Pages.ProductLanding;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Catalogue.Controllers;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.ViewModels;

[TestFixture]
public class ProductLandingPageControllerTests
{
    private Mock<ProductLandingPage> _mockPage;

    private ProductLandingPageController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockPage = new Mock<ProductLandingPage>(MockBehavior.Loose);

        _controller = new ProductLandingPageController();
    }

    [Test]
    public void Index_ReturnsAViewModelContainingTheContent()
    {
        // Act
        var result = _controller.Index(_mockPage.Object) as ViewResult;
        var model = result?.Model as ProductLandingPageViewModel;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentPage, Is.EqualTo(_mockPage.Object));
        });
    }

    [TearDown]
    public void TearDown()
    {
        _controller.Dispose();
        _controller = null;
    }
}
