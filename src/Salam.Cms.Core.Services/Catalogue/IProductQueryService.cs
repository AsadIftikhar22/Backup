namespace Salam.Cms.Core.Services.Catalogue;

using Salam.Cms.Shared.Models.Catalogue.Data;
using Salam.Cms.Shared.Models.Catalogue.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductQueryService
{
    Task<IReadOnlyDictionary<int, ProductSku>> GetSkusAsync(IEnumerable<int> ids, string language);

    Task<List<AttributeDefinition>> GetLabelsAsync(string language);

    Task<List<Category>> GetCategoriesAsync(int categoryId, string language);

    string GetLanguage(int storeId);
}