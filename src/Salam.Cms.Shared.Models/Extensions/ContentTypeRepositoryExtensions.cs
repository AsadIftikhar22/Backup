namespace Salam.Cms.Shared.Models.Extensions;

using EPiServer.DataAbstraction;
using System.Diagnostics.CodeAnalysis;

public static class ContentTypeRepositoryExtensions
{
    /// <summary>
    /// Attempts to retrieve a content type from the <see cref="IContentTypeRepository"/>.
    /// </summary>
    /// <param name="contentTypeRepository"></param>
    /// <param name="contentTypeId">The integer id of the content.</param>
    /// <param name="contentType">The resolved <see cref="ContentType"/> as an output param.</param>
    /// <returns></returns>
    public static bool TryGet(
        this IContentTypeRepository contentTypeRepository,
        int? contentTypeId,
        [NotNullWhen(true)] out ContentType? contentType)
    {
        contentType = default;

        if (!contentTypeId.HasValue)
        {
            return false;
        }

        contentType = contentTypeRepository.Load(contentTypeId.Value);

        return contentType != null;
    }
}
