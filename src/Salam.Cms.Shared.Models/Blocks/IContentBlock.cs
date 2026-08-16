namespace Salam.Cms.Shared.Models.Blocks;

using EPiServer.Core;

/// <summary>
/// Used to define a standard content block to be allowed in main content areas.
/// This combined with <see cref="ContentBlockUIDescriptor"/> simplify these declarations.
/// </summary>
public interface IContentBlock : IContentData
{
}