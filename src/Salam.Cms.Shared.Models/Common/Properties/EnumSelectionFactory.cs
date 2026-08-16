namespace Salam.Cms.Shared.Models.Common.Properties;

using EPiServer.Shell.ObjectEditing;
using Salam.Cms.Shared.Models.Extensions;

/// <summary>
/// Used in combination with <see cref="EnumEditorDescriptor{TEnum}"/> to allow the usage of enum properties on content types.
/// </summary>
/// <typeparam name="TEnum"></typeparam>
public class EnumSelectionFactory<TEnum> : ISelectionFactory
    where TEnum : Enum
{
    public IEnumerable<ISelectItem> GetSelections(
        ExtendedMetadata metadata)
    {
        var values = Enum.GetValues(typeof(TEnum));
        var selectItems = new List<SelectItem>();

        foreach (TEnum value in values)
        {
            selectItems.Add(new SelectItem
            {
                Text = value.ToDescription(),
                Value = value
            });
        }

        return selectItems;
    }
}