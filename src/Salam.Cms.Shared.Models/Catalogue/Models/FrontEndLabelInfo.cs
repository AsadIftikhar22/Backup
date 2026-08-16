namespace Salam.Cms.Shared.Models.Catalogue.Models;
public class FrontEndLabelInfo
{
    public string AttributeCode { get; set; }

    public string DefaultFrontEndLabel { get; set; }

    public string LabelCultureSpecific { get; set; }

    public string Language { get; set; }

    public string GetLabel()
    {
        return LabelCultureSpecific ?? DefaultFrontEndLabel;
    }
}
