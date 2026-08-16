namespace Salam.Cms.Shared.Models.Common.RichText;

using EPiServer.Cms.TinyMce.Core;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

[EditorDescriptorRegistration(TargetType = typeof(XhtmlString), EditorDescriptorBehavior = EditorDescriptorBehavior.OverrideDefault, UIHint = RichTextEditors.ReducedEditor)]
public class ReducedRichTextXhtmlStringEditorDescriptor : XhtmlStringEditorDescriptor
{
    public ReducedRichTextXhtmlStringEditorDescriptor(ServiceAccessor<TinyMceConfiguration> tinyMceConfiguration) : base(tinyMceConfiguration)
    {
    }

    public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
    {
        base.ModifyMetadata(metadata, attributes);

        if (!metadata.EditorConfiguration.ContainsKey("settings"))
        {
            return;
        }

        if (metadata.EditorConfiguration["settings"] is TinyMceSettings settings)
        {
            var formats = new
            {
                removeformat = new object[]
                {
                    new
                    {
                        selector = "b,em,font,strike,dfn,code,samp,kbd,var,cite,mark,q,del,ins,h1,h2,h3,h4,h5,h6,div,center,picture,source,table,tr,th,td",
                        remove = "all",
                        split = true,
                        expand = "false",
                        block_expand = true,
                        deep = true
                    }
                }
            };


            settings.AddSetting("formats", formats)
                    .ContentCss("/static/assets/css/editor.css")
                    .BlockFormats("Paragraph=p;")
                    .Toolbar(
                        "blocks",
                        "undo redo searchreplace | epi-link | bold italic underline | superscript subscript | removeformat | help",
                        "bullist numlist | epi-personalized-content | fullscreen")
                    .DisableMenubar();
        }
    }
}