namespace Salam.Cms.Tests.Common.Properties;

using Salam.Cms.Shared.Models.Common.Properties;
using System.Linq;

[TestFixture]
public class EnumSelectionFactoryTests
{
    [Test]
    public void GetSelections_AddsAllEnumValuesWithHumanReadableDescriptions()
    {
        // Arrange
        var enumSelectionFactory = new EnumSelectionFactory<EnumSelectionFactoryTestValues>();

        // Act
        var values = enumSelectionFactory.GetSelections(null!).ToList();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(values, Is.Not.Null);
            Assert.That(values, Has.Count.EqualTo(3));
            Assert.That(values[0].Text, Is.EqualTo("Single"));
            Assert.That(values[0].Value, Is.EqualTo(EnumSelectionFactoryTestValues.Single));
            Assert.That(values[1].Text, Is.EqualTo("Two Words"));
            Assert.That(values[1].Value, Is.EqualTo(EnumSelectionFactoryTestValues.TwoWords));
            Assert.That(values[2].Text, Is.EqualTo("Three Words Here"));
            Assert.That(values[2].Value, Is.EqualTo(EnumSelectionFactoryTestValues.ThreeWordsHere));
        });
    }
}
