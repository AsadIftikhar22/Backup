namespace Salam.Cms.UnitTests.Features.Blocks.FeaturedProductList;

using Moq;
using NUnit.Framework;
using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.ViewModels;
using System.Collections.Generic;

[TestFixture]
public class FeaturedProductListBlockViewModelTests
{
    private Mock<FeaturedProductListBlock> _mockBlock;

    [SetUp]
    public void Setup()
    {
        _mockBlock = new Mock<FeaturedProductListBlock>();
    }

    [Test]
    [TestCaseSource(typeof(FeaturedProductListBlockViewComponentTestCases), nameof(FeaturedProductListBlockViewComponentTestCases.BuildModelTestCases))]
    public void Constructor_SetsBlockProperties(string heading)
    {
        // Arrange
        _mockBlock.Setup(x => x.Heading).Returns(heading);

        // Act
        var viewModel = new FeaturedProductListBlockViewModel(_mockBlock.Object)
        {
            Products = new List<ProductSku>(),
            HandoffUrl = "https://example.com"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(viewModel.CurrentBlock, Is.EqualTo(_mockBlock.Object));
            Assert.That(viewModel.CurrentBlock.Heading, Is.EqualTo(heading));
            Assert.That(viewModel.Products, Is.Not.Null);
            Assert.That(viewModel.HandoffUrl, Is.EqualTo("https://example.com"));
        });
    }
}