namespace Salam.Cms.Web.Features.Hero.Abstract;

using EPiServer.Core;

/// <summary>
/// Used to define a hero block to be allowed in the hero content areas.
/// This combined with <see cref="HeroBlockUIDescriptor"/> simplify these declarations.
/// </summary>
public interface IHeroBlock : IContentData
{
}