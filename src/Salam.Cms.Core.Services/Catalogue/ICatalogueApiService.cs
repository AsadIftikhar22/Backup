namespace Salam.Cms.Core.Services.Catalogue;

using Salam.Cms.Shared.Models.Catalogue.Data;
using System.Collections.Generic;

public interface ICatalogueApiService
{
    Task<List<T>> FetchAndIndexDataAsync<T>(string apiUrl, string languageCode, string languageStore) where T : class;
    Task<List<AddOn>> FetchAndIndexAddOnsAsync(string apiUrl, string languageCode, string languageStore);
}