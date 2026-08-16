namespace Salam.Cms.Shared.Models.Common.Components;

using EPiServer.Framework.Blobs;
using Microsoft.Extensions.FileProviders;
using System;
using System.Threading.Tasks;

public class BlobOperations : IBlobOperations
{
    public async Task<IFileInfo> AsFileInfoAsync(Blob blob, DateTimeOffset? lastModified = null)
    {
        return await blob.AsFileInfoAsync(lastModified);
    }
}
