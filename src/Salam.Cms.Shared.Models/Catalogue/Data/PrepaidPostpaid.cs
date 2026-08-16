namespace Salam.Cms.Shared.Models.Catalogue.Data;

using Salam.Cms.Shared.Models.Catalogue.Data.Base;

public class PrepaidPostpaid : ServicePlanBase
{
    public PrepaidPostpaid()
    {
    }

    public PrepaidPostpaid(Item item, string language) : base(item, language)
    {
    }
}
