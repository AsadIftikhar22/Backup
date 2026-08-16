namespace Salam.Cms.Tests.Validation;

using Salam.Cms.Shared.Models.Validation;

[TestFixture]
public class NotNullOrEmptyAttributeTests
{
    [Test]
    [TestCaseSource(typeof(NotNullOrEmptyAttributeTestCases), nameof(NotNullOrEmptyAttributeTestCases.StringTestCases))]
    public void IsValid_CorrectlyValidatesStrings(string valueToValidate, bool expectedOutcome)
    {
        // Arrange
        var validator = new NotNullOrEmptyAttribute();

        // Act
        var isValid = validator.IsValid(valueToValidate);

        // Assert
        Assert.That(isValid, Is.EqualTo(expectedOutcome));
    }

    [Test]
    [TestCaseSource(typeof(NotNullOrEmptyAttributeTestCases), nameof(NotNullOrEmptyAttributeTestCases.GuidTestCases))]
    public void IsValid_CorrectlyValidatesGuids(Guid? valueToValidate, bool expectedOutcome)
    {
        // Arrange
        var validator = new NotNullOrEmptyAttribute();

        // Act
        var isValid = validator.IsValid(valueToValidate);

        // Assert
        Assert.That(isValid, Is.EqualTo(expectedOutcome));
    }

    [Test]
    [TestCaseSource(typeof(NotNullOrEmptyAttributeTestCases), nameof(NotNullOrEmptyAttributeTestCases.OtherObjectTestCases))]
    public void IsValid_CorrectlyValidatesOtherObjectsAsNotNull(object valueToValidate, bool expectedOutcome)
    {
        // Arrange
        var validator = new NotNullOrEmptyAttribute();

        // Act
        var isValid = validator.IsValid(valueToValidate);

        // Assert
        Assert.That(isValid, Is.EqualTo(expectedOutcome));
    }
}
