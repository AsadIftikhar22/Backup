namespace Salam.Cms.Shared.Models.Common.RichText
{
    using EPiServer.Cms.TinyMce.Core;
    using EPiServer.Core;
    using EPiServer.ServiceLocation;
    using EPiServer.Shell.ObjectEditing;
    using EPiServer.Shell.ObjectEditing.EditorDescriptors;
    using System.Collections.Generic;

    [EditorDescriptorRegistration(
        TargetType = typeof(XhtmlString),
        EditorDescriptorBehavior = EditorDescriptorBehavior.OverrideDefault,
        UIHint = RichTextEditors.HtmlEditor)]
    public class HtmlRichTextXHtmlStringEditorDescriptor : XhtmlStringEditorDescriptor
    {
        public HtmlRichTextXHtmlStringEditorDescriptor(ServiceAccessor<TinyMceConfiguration> tinyMceConfiguration)
            : base(tinyMceConfiguration)
        { }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<System.Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);

            if (!metadata.EditorConfiguration.TryGetValue("settings", out var cfg) ||
                cfg is not TinyMceSettings settings)
            {
                return;
            }

            var formats = new
            {
                removeformat = new[]
                {
                    new
                    {
                        selector = "b,em,dfn,code,samp,kbd,var,mark,q,del,ins,center,font,picture,source,table,tr,th,td",
                        remove = "all",
                        split = true,
                        expand = "false",
                        block_expand = true,
                        deep = true
                    }
                }
            };

            settings
                .AddSetting("formats", formats)
                .AddSetting("valid_children", "+body[style|div|script]")
                //.AddSetting("extended_valid_elements",
                //    "style[type],script[language|type|src]," +
                //    "span[class|style]," +
                //    "svg[*],g[*],path[*],circle[*],rect[*],line[*],polyline[*],polygon[*],ellipse[*],use[*]"
                //)
                .AddSetting("force_p_newlines", false)
                .AddSetting("force_br_newlines", false)
                .AddSetting("newline_behavior", "linebreak")
                .AddSetting("pad_empty_with_br", true)
                .AddPlugin("paste")
                .AddSetting("paste_as_text", true)
                .AddSetting("extended_valid_elements",
                  "style[type],script[language|type|src]," +
                    "span[class|style]," +
                    "svg[*],g[*],path[*],circle[*],rect[*],line[*],polyline[*],polygon[*],ellipse[*],use[*]"+
                      "span[*],svg[*],defs[*],g[*],path[*],line[*],circle[*],rect[*],polygon[*],polyline[*],ellipse[*],use[*],script[language|type|src|defer|async],iframe[src|width|height|style|name|title|allowfullscreen],div[class|id|style],a[href|target|class|style|id|title],div[class|id|style],span[class|style],div[id|class|style]")
                .AddSetting("allow_script_urls", "true")
                .ContentCss("/static/assets/css/editor.css")
                .AddPlugin("table anchor code")
                .BlockFormats("Paragraph=p;Header 2=h2;Header 3=h3;Header 4=h4;Block Quote=blockquote")
                .Toolbar(
                    "blocks",
                    "undo redo searchreplace | epi-link anchor | bold italic underline | superscript subscript | removeformat | help",
                    "alignleft aligncenter alignright alignjustify | bullist numlist | epi-personalized-content | code | fullscreen",
                    "image epi-image-editor | table tabledelete tableprops tablerowprops tablecellprops tableinsertrowbefore tableinsertrowafter tabledeleterow tableinsertcolbefore tableinsertcolafter tabledeletecol"
                );
        }
    }
}
