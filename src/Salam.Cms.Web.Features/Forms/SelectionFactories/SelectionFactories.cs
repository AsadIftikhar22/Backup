namespace Salam.Cms.Web.Features.Forms.SelectionFactories;
using EPiServer.Shell.ObjectEditing;


public class FontSizeSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        yield return new SelectItem { Text = "14px", Value = "14px" };
        yield return new SelectItem { Text = "16px", Value = "16px" };
        yield return new SelectItem { Text = "20px", Value = "20px" };
        yield return new SelectItem { Text = "22px", Value = "22px" };
        yield return new SelectItem { Text = "24px", Value = "24px" };
        yield return new SelectItem { Text = "28px", Value = "28px" };
        yield return new SelectItem { Text = "30px", Value = "30px" };
        yield return new SelectItem { Text = "36px", Value = "36px" };
        yield return new SelectItem { Text = "56px", Value = "56px" };
    }
}
public class FontWeightSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        yield return new SelectItem { Text = "400", Value = "400" };
        yield return new SelectItem { Text = "700", Value = "700" };
    }
}

public class LabelColorSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        yield return new SelectItem { Text = "Dark Green", Value = "#003831;" };
        yield return new SelectItem { Text = "Default", Value = "#2e7d32" };
    }
}

public class TextAreaHeightSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        yield return new SelectItem { Text = "100px", Value = "100px" };
        yield return new SelectItem { Text = "144px", Value = "144px" };
        yield return new SelectItem { Text = "150px", Value = "150px" };
        yield return new SelectItem { Text = "160px", Value = "160px" };
        yield return new SelectItem { Text = "180px", Value = "180px" };
        yield return new SelectItem { Text = "224px", Value = "224px" };
    }
}
