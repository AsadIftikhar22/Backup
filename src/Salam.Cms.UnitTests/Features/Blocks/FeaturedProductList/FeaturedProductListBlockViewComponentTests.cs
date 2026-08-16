namespace Salam.Cms.UnitTests.Features.Blocks.FeaturedProductList;

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
using Salam.Cms.Shared.Models.Catalogue.Data;
using Salam.Cms.Shared.Models.Catalogue.Enums;
using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.Components;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.ViewModels;
using Salam.Cms.Web.Features.Cookies.Services;
using Salam.Cms.Web.Features.LanguageSwitcher.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestFixture]
public class FeaturedProductListBlockViewComponentTests
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
    private CatalogueApiSettings _settings;

    [SetUp]
    public void Setup()
    {
        // Setup block mock with test data
        _mockBlock = new Mock<FeaturedProductListBlock>();
        _mockBlock.Setup(x => x.ProductType).Returns(ProductType.Postpaid);
        _mockBlock.Setup(x => x.QueryBehaviour).Returns(QueryBehaviourOption.ManualOnly);
        _mockBlock.Setup(x => x.ProductIds).Returns(new List<string> { "1", "2", "3" });
        _mockBlock.Setup(x => x.HandoffBehavior).Returns(HandoffOption.None);

        // Create settings and configure them
        _settings = new CatalogueApiSettings
        {
            PlanHandoffBaseUrl = "https://plan.example.com/{0}",
            DeviceHandoffBaseUrl = "https://device.example.com/{0}"
        };

        // Setup content loader
        _mockContentLoader = new Mock<IContentLoader>();

        // Setup Find client
        _findClient = new Mock<IClient>();

        // Setup product query service and mock response
        _productQueryService = new Mock<IProductQueryService>();
        var mockProducts = new Dictionary<int, ProductSku>
        {
            { 1, new ProductSku { Id = 1, Name = "Product 1" } },
            { 2, new ProductSku { Id = 2, Name = "Product 2" } },
            { 3, new ProductSku { Id = 3, Name = "Product 3" } }
        };
        _productQueryService.Setup(x => x.GetSkusAsync(It.IsAny<List<int>>(), It.IsAny<string>()))
            .ReturnsAsync(mockProducts);

        // Setup catalogue API settings
        _catalogueApiSettings = new Mock<IOptions<CatalogueApiSettings>>();
        _catalogueApiSettings.Setup(x => x.Value).Returns(_settings);

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
        var englishLanguageBranch = new LanguageBranch("en") { Enabled = true };
        _mockLanguageBranchRepository.Setup(x => x.ListEnabled())
            .Returns(new List<LanguageBranch> { englishLanguageBranch });

        // Create the component under test
        _component = new FeaturedProductListBlockViewComponent(
            _mockContentLoader.Object,
            _findClient.Object,
            _productQueryService.Object,
            _catalogueApiSettings.Object,
            _languageService);
    }

    [Test]
    [TestCaseSource(typeof(FeaturedProductListBlockViewComponentTestCases), nameof(FeaturedProductListBlockViewComponentTestCases.BuildModelTestCases))]
    public async Task Invoke_CorrectlyBuildsTheModelAndReturnsItWithAViewResponse(
        string heading)
    {
        // Arrange
        _mockBlock.Setup(x => x.Heading).Returns(heading);

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
            Assert.That(model.Products, Is.Not.Null);
            Assert.That(model.Products.Count, Is.EqualTo(3));
            Assert.That(model.HandoffUrl, Is.EqualTo(string.Empty));
        });
    }

    [Test]
    public async Task Invoke_WithPlanHandoff_SetsCorrectHandoffUrl()
    {
        // Arrange
        _mockBlock.Setup(x => x.HandoffBehavior).Returns(HandoffOption.Plan);

        // Act
        var result = await _component.InvokeAsync(_mockBlock.Object);
        var viewResult = result as ViewViewComponentResult;
        var model = viewResult?.ViewData?.Model as FeaturedProductListBlockViewModel;

        // Assert
        Assert.That(model.HandoffUrl, Is.EqualTo("https://plan.example.com/en"));
    }

    [Test]
    public async Task Invoke_WithDeviceHandoff_SetsCorrectHandoffUrl()
    {
        // Arrange
        _mockBlock.Setup(x => x.HandoffBehavior).Returns(HandoffOption.Device);

        // Act
        var result = await _component.InvokeAsync(_mockBlock.Object);
        var viewResult = result as ViewViewComponentResult;
        var model = viewResult?.ViewData?.Model as FeaturedProductListBlockViewModel;

        // Assert
        Assert.That(model.HandoffUrl, Is.EqualTo("https://device.example.com/en"));
    }
}