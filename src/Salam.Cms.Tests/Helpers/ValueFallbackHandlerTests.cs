namespace Salam.Cms.Tests.Helpers;

using EPiServer.Core;
using Salam.Cms.Shared.Models.Helpers;

[TestFixture]
public class ValueFallbackHandlerTests
{
    private ValueFallbackHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _handler = new ValueFallbackHandler();
    }

    [Test]
    [TestCaseSource(typeof(ValueFallbackHandlerTestCases), nameof(ValueFallbackHandlerTestCases.TextTestCases))]
    public void GetBest_WhenGivenTwoStrings_CorrectlyRetrievesTheFirstViableString(
        string firstText,
        string secondText,
        string expectedText)
    {
        // Act
        var actualText = _handler.GetBest(firstText, secondText);

        // Arrange
        Assert.That(actualText, Is.EqualTo(expectedText));
    }

    [Test]
    [TestCaseSource(typeof(ValueFallbackHandlerTestCases), nameof(ValueFallbackHandlerTestCases.ContentReferenceTestCases))]
    public void GetBest_WhenGivenTwoContentReferences_CorrectlyReturnsTheFirstViableValueOrAnEmptyReference(
        ContentReference firstReference,
        ContentReference secondReference,
        ContentReference expectedReference)
    {
        // Act
        var actualValue = _handler.GetBest(firstReference, secondReference);

        // Arrange
        Assert.That(actualValue.CompareToIgnoreWorkID(expectedReference), Is.True);
    }
}