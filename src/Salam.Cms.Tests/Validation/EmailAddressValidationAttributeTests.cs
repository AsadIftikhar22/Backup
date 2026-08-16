namespace Salam.Cms.Tests.Validation;

using JetBrains.Annotations;
using Salam.Cms.Shared.Models.Validation;

[TestFixture]
public sealed class EmailAddressValidationAttributeTests
{
    [Test]
    [TestCase(null, true)]
    [TestCase("", true)]
    [TestCase(" ", false)]
    [TestCase("not an email address", false)]
    [TestCase("joe.bloggs.at.somewhere.com", false)]
    [TestCase("joe.bloggs@somewhere.com", true)]
    public void IsValid_CorrectlyReturnsAValidState([CanBeNull] object value, bool expectedResult)
    {
        // Arrange
        var validator = new EmailAddressValidationAttribute();

        // Act
        var result = validator.IsValid(value);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}