namespace Salam.Cms.Shared.Models.Images;

using System;

/// <summary>
/// Result of a proxied image request
/// </summary>
public record ProxyImageResult(byte[] Bytes, string ContentType)
{
    /// <summary>
    /// Represents an invalid request
    /// </summary>
    public static readonly ProxyImageResult Invalid = new(Array.Empty<byte>(), string.Empty);

    /// <summary>
    /// Represents a failed request
    /// </summary>
    public static readonly ProxyImageResult Failed = new(Array.Empty<byte>(), string.Empty);

    /// <summary>
    /// Indicates whether the result came from cache
    /// </summary>
    public bool FromCache { get; init; } = false;

    /// <summary>
    /// Indicates whether the result is successful
    /// </summary>
    public bool Success => Bytes.Length > 0;
}
