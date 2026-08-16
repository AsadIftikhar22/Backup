namespace Salam.Cms.Tests.Extensions;

using EPiServer.Core;
using Salam.Cms.Shared.Models.Extensions;

[TestFixture]
public sealed class XHtmlStringExtensionsTests
{
    [Test]
    [TestCaseSource(typeof(XHtmlStringExtensionsTestCases), nameof(XHtmlStringExtensionsTestCases.GetNullOrEmptyRichTextTestCases))]
    public void IsNullOrEmpty_ReturnsTrueWhenRichTextIsNullOrEmpty(XhtmlString richText)
    {
        var isNullOrEmpty = richText.IsNullOrEmpty();

        // Assert
        Assert.That(isNullOrEmpty, Is.True);
    }

    [Test]
    public void IsNullOrEmpty_ReturnsTrueWhenRichTextHasContent()
    {
        // Arrange
        var richText = new XhtmlString("<p>Hello World</p>");

        // Act
        var isNullOrEmpty = richText.IsNullOrEmpty();

        // Assert
        Assert.That(isNullOrEmpty, Is.False);
    }

    [Test]
    public void ReplacePlaceholdersTest()
    {
        // Arrange
        var richText = new XhtmlString("<p><strong>{{total}} result</strong> returned for <strong>{{term}}</strong></p>");
        var replacements = new Dictionary<string, string>
                {
                    {"total", "2"},
                    {"term", "insurance"}
                };
        var expectedResult = "<p><strong>2 result</strong> returned for <strong>insurance</strong></p>";

        // Act
        var result = richText.ReplacePlaceholders(replacements);

        // Assert
        Assert.That(result.ToString(), Is.EqualTo(expectedResult));
    }
}