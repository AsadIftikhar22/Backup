namespace Salam.Cms.Shared.Models.Catalogue.Data;

using Salam.Cms.Shared.Models.Catalogue.Data.Base;

public class Product : ItemBase
{
    public string TypeId { get; set; } = string.Empty;
}