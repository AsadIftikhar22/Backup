namespace Salam.Cms.Core.Services.Images;

using Microsoft.AspNetCore.Html;
using Salam.Cms.Shared.Models.Media;
using System.Threading.Tasks;

public interface IImageUtilityService
{
    public Task<HtmlString> ConvertImageToRawContentAsync(VectorImageContent vectorImageContent);
}
