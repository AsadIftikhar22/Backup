namespace Salam.Cms.Web.Features.Hero.Editor;

using EPiServer.Shell;
using Salam.Cms.Web.Features.Hero.Abstract;

/// <summary>
/// Used to help Optimizely CMS UI Recognize the <see cref="IHeroBlock"/> interface.
/// This allows us to simplify which blocks are allowed in hero content areas.
/// </summary>
[UIDescriptorRegistration]
public class HeroBlockUIDescriptor : UIDescriptor<IHeroBlock>
{
}