namespace Salam.Cms.Tests.Helpers;

using Salam.Cms.Shared.Models.Helpers;

[TestFixture]
public class PlaceholderReplacerTests
{
    private IPlaceholderReplacer _replacer;

    [SetUp]
    public void Setup()
    {
        _replacer = new PlaceholderReplacer();
    }

    [Test]
    [TestCase("*something*", PlaceholderType.Asterisk, "<span class=\"text--bright-blue-2\">something</span>")]
    [TestCase("%something%", PlaceholderType.Percentage, "<span class=\"text--bright-pink\">something</span>")]
    [TestCase("*something* and then something *again*", PlaceholderType.Asterisk, "<span class=\"text--bright-blue-2\">something</span> and then something <span class=\"text--bright-blue-2\">again</span>")]
    [TestCase("%something% and then something %again%", PlaceholderType.Percentage, "<span class=\"text--bright-pink\">something</span> and then something <span class=\"text--bright-pink\">again</span>")]
    [TestCase("*something* and then something *not quite there", PlaceholderType.Asterisk, "<span class=\"text--bright-blue-2\">something</span> and then something *not quite there")]
    [TestCase("%something% and then something %not quite there", PlaceholderType.Percentage, "<span class=\"text--bright-pink\">something</span> and then something %not quite there")]
    [TestCase("*", PlaceholderType.Asterisk, "*")]
    [TestCase("%", PlaceholderType.Percentage, "%")]
    public void CanReplace(string source, PlaceholderType placeholderType, string expectedResult)
    {
        var actualResult = _replacer.ReplacePlaceholders(source, placeholderType);
        Assert.That(expectedResult, Is.EqualTo(actualResult));
    }
}
