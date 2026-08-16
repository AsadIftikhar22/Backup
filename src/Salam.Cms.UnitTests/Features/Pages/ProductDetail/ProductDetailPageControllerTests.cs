namespace Salam.Cms.UnitTests.Features.Pages.ProductDetail;

using EPiServer;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Catalogue.Controllers;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.ViewModels;

[TestFixture]
public class ProductDetailPageControllerTests
{
    private Mock<ProductDetailPage> _mockPage;

    private Mock<IContentLoader> _mockContentLoader;

    private ProductDetailPageController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockPage = new Mock<ProductDetailPage>(MockBehavior.Loose);

        _mockContentLoader = new Mock<IContentLoader>();

        _controller = new ProductDetailPageController(_mockContentLoader.Object);
    }

    [Test]
    public void Index_ReturnsAViewModelContainingTheContent()
    {
        // Act
        var result = _controller.Index(_mockPage.Object) as ViewResult;
        var model = result?.Model as ProductDetailPageViewModel;

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
