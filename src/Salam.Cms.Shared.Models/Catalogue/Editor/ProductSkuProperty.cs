namespace Salam.Cms.Shared.Models.Catalogue.Editor;

using EPiServer.Core;
using EPiServer.PlugIn;
using Salam.Cms.Shared.Models.Catalogue.Models;

/// <summary>
/// An essential class for defining an <see cref="PropertyList{TItem}"/> property against the <see cref="ProductSku"/>
/// <para>TItem is <see cref="ProductSku"/></para>
/// </summary>
[PropertyDefinitionTypePlugIn]
public sealed class ProductSkuProperty : PropertyList<ProductSku>
{
}