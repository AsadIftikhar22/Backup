namespace Salam.Cms.Tests.Validation;

using JetBrains.Annotations;
using Salam.Cms.Shared.Models.Validation;

[TestFixture]
public sealed class UrlValidationAttributeTests
{
    [Test]
    [TestCase(null, true)]
    [TestCase("", true)]
    [TestCase(" ", false)]
    [TestCase("not a url", false)]
    [TestCase("/relative-url/example/", false)]
    [TestCase("/relative-url/example/?q=123", false)]
    [TestCase("http://www.example.com/absolute-url/example/", true)]
    [TestCase("http://www.example.com/absolute-url/example/?q=123", true)]
    [TestCase("https://www.example.com/absolute-url/example/", true)]
    [TestCase("https://www.example.com/absolute-url/example/?q=123", true)]
    [TestCase("ftp://www.example.com/ftp-example/", false)]
    [TestCase("ftp://www.example.com/ftp-example/", false)]
    public void IsValid_CorrectlyReturnsAValidState([CanBeNull] object value, bool expectedResult)
    {
        // Arrange
        var validator = new UrlValidationAttribute();

        // Act
        var result = validator.IsValid(value);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}