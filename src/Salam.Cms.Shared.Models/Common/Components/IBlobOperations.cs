namespace Salam.Cms.Shared.Models.Common.Components;

using EPiServer.Framework.Blobs;
using Microsoft.Extensions.FileProviders;
using System;
using System.Threading.Tasks;

public interface IBlobOperations
{
    Task<IFileInfo> AsFileInfoAsync(Blob blob, DateTimeOffset? lastModified = null);
}
