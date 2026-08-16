namespace Salam.Cms.UnitTests.Features.Blocks.ProductSelector;

using EPiServer;
using EPiServer.DataAbstraction;
using EPiServer.Find;
using EPiServer.Globalization;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Salam.Cms.Core.Services.Catalogue;
using Salam.Cms.Core.Settings.Configuration;
using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.Components;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.ViewModels;
using Salam.Cms.Web.Features.Cookies.Services;
using Salam.Cms.Web.Features.LanguageSwitcher.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestFixture]
public class ProductSelectorBlockViewComponentTests
{
    private Mock<IContentLoader> _mockContentLoader;
    private Mock<FeaturedProductListBlock> _mockBlock;
    private FeaturedProductListBlockViewComponent _component;
    private Mock<IClient> _findClient;
    private Mock<IProductQueryService> _productQueryService;
    private Mock<IOptions<CatalogueApiSettings>> _catalogueApiSettings;
    private LanguageService _languageService;
    private Mock<ILanguageBranchRepository> _mockLanguageBranchRepository;
    private Mock<ICookieService> _mockCookieService;
    private Mock<IUpdateCurrentLanguage> _mockUpdateCurrentLanguage;

    [SetUp]
    public void Setup()
    {
        _mockBlock = new Mock<FeaturedProductListBlock>();
        _mockContentLoader = new Mock<IContentLoader>();
        _findClient = new Mock<IClient>();
        _productQueryService = new Mock<IProductQueryService>();
        _catalogueApiSettings = new Mock<IOptions<CatalogueApiSettings>>();
        _catalogueApiSettings.Setup(x => x.Value).Returns(new CatalogueApiSettings());

        // Create mocks for LanguageService dependencies
        _mockLanguageBranchRepository = new Mock<ILanguageBranchRepository>();
        _mockCookieService = new Mock<ICookieService>();
        _mockUpdateCurrentLanguage = new Mock<IUpdateCurrentLanguage>();

        // Create actual LanguageService with mocked dependencies
        _languageService = new LanguageService(
            _mockLanguageBranchRepository.Object,
            _mockCookieService.Object,
            _mockUpdateCurrentLanguage.Object);

        // Setup language service to return a culture
        _mockLanguageBranchRepository.Setup(x => x.ListEnabled())
            .Returns(new List<LanguageBranch> {
                new LanguageBranch("en") {
                    Enabled = true
                }
            });

        _component = new FeaturedProductListBlockViewComponent(
            _mockContentLoader.Object,
            _findClient.Object,
            _productQueryService.Object,
            _catalogueApiSettings.Object,
            _languageService);
    }

    [Test]
    [TestCaseSource(typeof(ProductSelectorBlockViewComponentTestCases), nameof(ProductSelectorBlockViewComponentTestCases.BuildModelTestCases))]
    public async Task Invoke_CorrectlyBuildsTheModelAndReturnsItWithAViewResponse(
        string heading)
    {
        // Arrange
        _mockBlock.Setup(x => x.Heading).Returns(heading);

        // Setup ProductQuery to return empty results to avoid null reference exceptions
        _productQueryService.Setup(x => x.GetSkusAsync(It.IsAny<List<int>>(), It.IsAny<string>()))
            .ReturnsAsync(new Dictionary<int, ProductSku>());

        // Act
        var result = await _component.InvokeAsync(_mockBlock.Object);
        var viewResult = result as ViewViewComponentResult;
        var model = viewResult?.ViewData?.Model as FeaturedProductListBlockViewModel;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(viewResult, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentBlock.Heading, Is.EqualTo(heading));
        });
    }
}
