namespace Salam.Cms.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;

public class KeyValue
{
    [Display(Name = nameof(Key))]
    public virtual string Key { get; set; }

    [Display(Name = nameof(Value))]
    public virtual string Value { get; set; }
}
