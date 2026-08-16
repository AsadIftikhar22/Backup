namespace Salam.Cms.Shared.Models.Catalogue.Models;

using EPiServer.Shell.ObjectEditing;
using Salam.Cms.Shared.Models.Common.Properties;
using System.ComponentModel.DataAnnotations;

public sealed class QueryParameter
{
    public QueryParameter() { }

    [Display(
        Name = "Key",
        Order = 10)]
    [Required]
    [AutoSuggestSelection(typeof(QueryParameterSelectionQuery), AllowCustomValues = false)]
    public string Key { get; set; } = string.Empty;

    [Display(
        Name = "Value",
        Description = "If key is price then use '<' between numbers",
        Order = 20)]
    public string Value { get; set; } = string.Empty;
}
