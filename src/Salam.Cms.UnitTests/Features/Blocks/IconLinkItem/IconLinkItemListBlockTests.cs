namespace Salam.Cms.UnitTests.Features.Blocks.IconLinkItem;

using EPiServer.Core;
using NUnit.Framework;
using Salam.Cms.Web.Features.IconLinks.Models;

[TestFixture]
public class IconLinkItemListBlockTests
{
    [Test]
    public void DefaultValues_ShouldBeNull()
    {
        var block = new IconLinkItemListBlock();
        Assert.Multiple(() =>
        {
            Assert.That(block.Heading, Is.Null, "Heading should be null by default");
            Assert.That(block.Description, Is.Null, "Description should be null by default");
            Assert.That(block.Items, Is.Null, "Items should be null by default");
        });
    }

    [Test]
    [TestCaseSource(typeof(IconLinkItemListBlockTestCases), nameof(IconLinkItemListBlockTestCases.CanSetPropertiesTestCases))]
    public void Should_SetProperties_Correctly(string heading, string descriptionHtml, bool assignItems)
    {
        // Arrange
        var block = new IconLinkItemListBlock();

        // Act
        block.Heading = heading;
        block.Description = descriptionHtml != null ? new XhtmlString(descriptionHtml) : null;
        block.Items = assignItems ? new ContentArea() : null;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(block.Heading, Is.EqualTo(heading));

            if (descriptionHtml == null)
            {
                Assert.That(block.Description, Is.Null);
            }
            else
            {
                Assert.That(block.Description?.ToString(), Is.EqualTo(descriptionHtml));
            }

            if (assignItems)
            {
                Assert.That(block.Items, Is.Not.Null);
            }
            else
            {
                Assert.That(block.Items, Is.Null);
            }
        });
    }
}