namespace Salam.Cms.Shared.Models.Common.Properties;

using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

/// <summary>
/// Used in combination with <see cref="EnumSelectionFactory{TEnum}"/> to allow the usage of enum properties on content types.
/// </summary>
/// <typeparam name="TEnum"></typeparam>
public class EnumEditorDescriptor<TEnum> : EditorDescriptor
    where TEnum : Enum
{
    public override void ModifyMetadata(
        ExtendedMetadata metadata,
        IEnumerable<Attribute> attributes)
    {
        SelectionFactoryType = typeof(EnumSelectionFactory<TEnum>);

        ClientEditingClass = "epi-cms/contentediting/editors/SelectionEditor";

        base.ModifyMetadata(metadata, attributes);
    }
}