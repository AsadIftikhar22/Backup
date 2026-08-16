namespace Salam.Cms.UnitTests.Features.Components.Common;

using EPiServer;
using EPiServer.SpecializedProperties;
using EPiServer.Web.Routing;
using Salam.Cms.Shared.Models.Common.Components;

[TestFixture]
public sealed class LinkModelConverterTests
{
    private ILinkModelConverter _linkModelConverter;
    private Mock<IUrlResolver> _mockUrlResolver;

    [SetUp]
    public void Setup()
    {
        _mockUrlResolver = new Mock<IUrlResolver>();
        _mockUrlResolver.Setup(x => x.GetUrl(It.IsAny<UrlBuilder>(), It.IsAny<UrlResolverArguments>()))
                        .Returns<UrlBuilder, UrlResolverArguments>((a, b) => a.ToString());

        _linkModelConverter = new LinkModelConverter(_mockUrlResolver.Object);
    }

    [Test]
    public void Build_WhenNoLinksEmptyListIsReturned()
    {
        //  Act
        var modelList = _linkModelConverter.ConvertToModelCollection(new LinkItemCollection());

        //  Assert
        Assert.That(modelList.Any, Is.False);
    }

    [Test]
    public void Build_WhenCollectionHasItemsReturnsList()
    {
        //  Arrange
        var links = new LinkItemCollection
        {
            new() { Href = "https://www.salam.sa", Text = "Salam", Title = "Salam Title", Target = "_blank" },
            new() { Href = "https://www.salam.sa/place", Text = "Place", Title = "Place Title", Target = "_top" },
            new() { Href = "https://www.salam.sa/event", Text = "Event" }
        };

        //  Act
        var modelList = _linkModelConverter.ConvertToModelCollection(links);

        //  Assert
        Assert.Multiple(() =>
        {
            Assert.That(modelList, Has.Count.EqualTo(3));
            Assert.That(modelList[0].Text, Is.EqualTo("Salam"));
            Assert.That(modelList[0].Title, Is.EqualTo("Salam Title"));
            Assert.That(modelList[0].Target, Is.EqualTo("_blank"));
            Assert.That(modelList[0].Url, Is.EqualTo("https://www.salam.sa/"));
            Assert.That(modelList[1].Text, Is.EqualTo(links[1].Text));
            Assert.That(modelList[1].Title, Is.EqualTo("Place Title"));
            Assert.That(modelList[1].Target, Is.EqualTo("_top"));
            Assert.That(modelList[1].Url, Is.EqualTo("https://www.salam.sa/place"));
            Assert.That(modelList[2].Text, Is.EqualTo("Event"));
            Assert.That(modelList[2].Title, Is.EqualTo(string.Empty));
            Assert.That(modelList[2].Target, Is.EqualTo(string.Empty));
            Assert.That(modelList[2].Url, Is.EqualTo("https://www.salam.sa/event"));
        });
    }

    [Test]
    public void Build_WhenALinkIsAMailLinkThenUrlResolverIsNotUsed()
    {
        //  Arrange
        var links = new LinkItemCollection
        {
            new() { Href = "mailto:joe.bloggs@example.com", Text = "Email This" }
        };

        //  Act
        var modelList = _linkModelConverter.ConvertToModelCollection(links);

        //  Assert
        Assert.Multiple(() =>
        {
            _mockUrlResolver.Verify(x => x.GetUrl(It.IsAny<UrlBuilder>(), It.IsAny<UrlResolverArguments>()), Times.Never);
            Assert.That(modelList, Has.Count.EqualTo(1));
            Assert.That(modelList[0].Text, Is.EqualTo("Email This"));
            Assert.That(modelList[0].Url, Is.EqualTo("mailto:joe.bloggs@example.com"));
        });
    }
}