using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using Salam.Cms.Web.Features.DXPB2BTable.Models;

[EditorDescriptorRegistration(TargetType = typeof(B2BHalfCellGenericBlock), UIHint = "FirstHeaderHalfCell")]
public class FirstHeaderHalfCellEditorDescriptor : EditorDescriptor
{
    public FirstHeaderHalfCellEditorDescriptor()
    {
        // Path is relative to ClientResources folder, no .js extension
        ClientEditingClass = "js/editors/firstHeaderHalfCell";
    }
}
