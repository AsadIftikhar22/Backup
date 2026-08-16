namespace Salam.Cms.Shared.Models.Extensions;

using EPiServer.Core;
using System.Diagnostics.CodeAnalysis;

public static class PageReferenceExtensions
{
    /// <summary>
    /// Determines if a <see cref="PageReference"/> is null or empty.
    /// </summary>
    /// <param name="pageReference">The <see cref="PageReference"/> to validate.</param>
    /// <returns>true or false</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this PageReference? pageReference)
    {
        // This wrapper methods adds consistent language and helps intellisense understand object states.
        return PageReference.IsNullOrEmpty(pageReference);
    }
}