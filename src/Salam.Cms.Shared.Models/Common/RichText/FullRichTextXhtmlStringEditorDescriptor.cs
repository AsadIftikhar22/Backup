namespace Salam.Cms.Shared.Models.Common.RichText;

using EPiServer.Cms.TinyMce.Core;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

[EditorDescriptorRegistration(TargetType = typeof(XhtmlString), EditorDescriptorBehavior = EditorDescriptorBehavior.OverrideDefault, UIHint = RichTextEditors.FullEditor)]
public class FullRichTextXhtmlStringEditorDescriptor : XhtmlStringEditorDescriptor
{
    public FullRichTextXhtmlStringEditorDescriptor(ServiceAccessor<TinyMceConfiguration> tinyMceConfiguration) : base(tinyMceConfiguration)
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
                        selector = "b,em,dfn,code,samp,kbd,var,mark,q,del,ins,div,center,font,picture,source",
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
                    .AddPlugin("table anchor code")
                    .BlockFormats(
                        "Paragraph=p;Header 2=h2;Header 3=h3;Header 4=h4;Header 5=h5;Header 6=h6;Block Quote=blockquote")
                    .Toolbar(
                        "blocks",
                        "undo redo searchreplace | epi-link anchor | bold italic underline | superscript subscript | removeformat | help",
                        "alignleft aligncenter alignright alignjustify | bullist numlist | epi-personalized-content | code | fullscreen",
                        "image epi-image-editor | table tabledelete tableprops tablerowprops tablecellprops tableinsertrowbefore tableinsertrowafter tabledeleterow tableinsertcolbefore tableinsertcolafter tabledeletecol");
        }
    }
}