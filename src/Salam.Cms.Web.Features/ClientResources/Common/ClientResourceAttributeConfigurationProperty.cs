namespace Salam.Cms.Web.Features.ClientResources.Common;

using EPiServer.Core;
using EPiServer.PlugIn;

/// <summary>
/// An essential class for defining an <see cref="PropertyList{TItem}"/> property against the <see cref="ClientResourceAttributeConfiguration"/>
/// <para>TItem is <see cref="ClientResourceAttributeConfiguration"/></para>
/// </summary>
[PropertyDefinitionTypePlugIn]
public sealed class ClientResourceAttributeConfigurationProperty : PropertyList<ClientResourceAttributeConfiguration>
{
}
