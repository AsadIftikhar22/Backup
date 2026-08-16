namespace Salam.Cms.UnitTests.Features.Blocks.IconLinkItem;

using EPiServer.Core;
using EPiServer.SpecializedProperties;
using Salam.Cms.Web.Features.IconLinks.Models;

[TestFixture]
public class IconLinkItemBlockTests
{
    [Test]
    public void DefaultValues_ShouldBeNull()
    {
        var block = new IconLinkItemBlock();
        Assert.Multiple(() =>
        {
            Assert.That(block.Icon, Is.Null, "Icon should be null by default");
            Assert.That(block.Link, Is.Null, "Link should be null by default");
        });
    }

    [Test]
    [TestCaseSource(typeof(IconLinkItemBlockTestCases), nameof(IconLinkItemBlockTestCases.CanSetPropertiesTestCases))]
    public void Should_SetProperties_Correctly(ContentReference icon, string href, string text)
    {
        // Arrange
        var block = new IconLinkItemBlock();

        // Act
        block.Icon = icon;
        block.Link = new LinkItem { Href = href, Text = text };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(block.Icon, Is.EqualTo(icon));
            Assert.That(block.Link?.Href, Is.EqualTo(href));
            Assert.That(block.Link?.Text, Is.EqualTo(text));
        });
    }
}