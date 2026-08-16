namespace Salam.Cms.Shared.Models.Extensions;

using System.Diagnostics.CodeAnalysis;

public static class ListExtensions
{
    /// <summary>
    /// Determines if a <see cref="IList{T}"/> is null or empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The <see cref="IList{T}"/> to validate.</param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IList<T>? list)
    {
        return list == null || list.Count == 0;
    }
}