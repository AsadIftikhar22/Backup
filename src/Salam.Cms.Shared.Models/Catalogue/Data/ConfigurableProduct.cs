namespace Salam.Cms.Shared.Models.Catalogue.Data;

using System.Collections.Generic;

public class ConfigurableProduct : Product
{
    public List<int> ConfigurableProductLinks { get; set; } = new List<int>();
}