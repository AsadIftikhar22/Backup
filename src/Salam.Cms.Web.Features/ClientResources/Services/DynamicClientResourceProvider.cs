using Salam.Cms.Web.Features.Settings.Models;

namespace Salam.Cms.Web.Features.ClientResources.Services;

using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework.Web.Resources;
using EPiServer.Web.Routing;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Web.Features.ClientResources.Abstract;
using Salam.Cms.Web.Features.ClientResources.Common;
using Salam.Cms.Web.Features.ClientResources.Enums;
using Salam.Cms.Web.Features.Embed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ClientResourceSettings = ClientResourceSettings;

[ClientResourceProvider]
public class DynamicClientResourceProvider : IClientResourceProvider
{
    private readonly IContentLoader _contentLoader;
    private readonly IContentTypeRepository _contentTypeRepository;
    private readonly IContentModelUsage _contentModelUsage;
    private readonly IUrlResolver _urlResolver;
    private readonly ISettingsManager _settingsManager;

    public DynamicClientResourceProvider(
        IContentLoader contentLoader,
        IContentTypeRepository contentTypeRepository,
        IContentModelUsage contentModelUsage,
        IUrlResolver urlResolver,
        ISettingsManager settingsManager)
    {
        _contentLoader = contentLoader;
        _contentTypeRepository = contentTypeRepository;
        _contentModelUsage = contentModelUsage;
        _urlResolver = urlResolver;
        _settingsManager = settingsManager;
    }

    public IEnumerable<ClientResource> GetClientResources()
    {
        var clientResources = new List<IClientResourceInclude>();

        var clientResourceSettings = _settingsManager.GetSettings<ClientResourceSettings>();

        if (clientResourceSettings?.ClientResources != null)
        {
            clientResources = clientResourceSettings.ClientResources.FilteredItems
                .Select(x => _contentLoader.Get<IClientResourceInclude>(x.ContentLink))
                .ToList();
        }

        var contentType = _contentTypeRepository.Load<EmbedBlock>();

        var embedClientResourceContent = _contentModelUsage.ListContentOfContentType(contentType);

        var distinctEmbedContentReferences = embedClientResourceContent.DistinctBy(x => x.ContentLink.ToReferenceWithoutVersion())
            .Select(x => x.ContentLink.ToReferenceWithoutVersion());

        foreach (var embedRef in distinctEmbedContentReferences)
        {
            var embedBlock = _contentLoader.Get<EmbedBlock>(embedRef);

            var embedClientResources = embedBlock.ClientResources?.FilteredItems
                .Select(x => _contentLoader.Get<IClientResourceInclude>(x.ContentLink));

            clientResources.Add(embedBlock);

            if (embedClientResources != null)
            {
                clientResources.AddRange(embedClientResources);
            }
        }

        if (!clientResources.Any())
        {
            yield break;
        }

        foreach (var resource in clientResources)
        {

            yield return CreateClientResource(resource);
        }
    }

    private ClientResource CreateClientResource(IClientResourceInclude resource)
    {
        switch (resource)
        {
            case IExternalResourceInclude externalResource:
                return CreateExternalClientResource(externalResource);
            case IEmbedResourceInclude embedResource:
                return CreateEmbedClientResource(embedResource);
            case IScriptResourceInclude scriptResource:
                return CreateScriptClientResource(scriptResource);
            case IStyleResourceInclude styleResource:
                return CreateStyleClientResource(styleResource);
            default:
                throw new NotSupportedException($"Resource Type {resource.GetType().Name} is not supported.");
        }
    }

    private ClientResource CreateExternalClientResource(IExternalResourceInclude externalResource)
    {
        var clientResource = new ClientResource
        {
            Name = $"{ClientResourceConstants.RegisteredResourcePrefix}{(externalResource as IContent)?.ContentLink.ID}",
            Path = externalResource.ExternalUrl,
            IsMinified = externalResource.IsMinified,
            Attributes = GetAttributes(externalResource),
            Dependencies = GetDependencies(externalResource)
        };

        switch (externalResource.ResourceType)
        {
            case ClientResourceTypeOption.JavaScript:
                clientResource.ResourceType = ClientResourceType.Script;
                break;
            case ClientResourceTypeOption.Stylesheet:
                clientResource.ResourceType = ClientResourceType.Style;
                break;
            default:
                throw new NotSupportedException($"Resource Type {externalResource.ResourceType} is not supported.");
        }

        return clientResource;
    }

    private static ClientResource CreateEmbedClientResource(IEmbedResourceInclude embedResource)
    {
        return new ClientResource
        {
            Name = $"{ClientResourceConstants.RegisteredResourcePrefix}{(embedResource as IContent)?.ContentLink.ID}",
            ResourceType = ClientResourceType.Html,
            InlineContent = embedResource.EmbedContent // TODO: Sanitize this content and ensure it's safe to render.
        };
    }

    private ClientResource CreateScriptClientResource(IScriptResourceInclude scriptResource)
    {
        return new ClientResource
        {
            Name = $"{ClientResourceConstants.RegisteredResourcePrefix}{(scriptResource as IContent)?.ContentLink.ID}",
            ResourceType = ClientResourceType.Script,
            Path = _urlResolver.GetUrl((scriptResource as IContent)?.ContentLink),
            IsMinified = scriptResource.IsMinified,
            Attributes = GetAttributes(scriptResource),
            Dependencies = GetDependencies(scriptResource)
        };
    }

    private ClientResource CreateStyleClientResource(IStyleResourceInclude styleResource)
    {
        return new ClientResource
        {
            Name = $"{ClientResourceConstants.RegisteredResourcePrefix}{(styleResource as IContent)?.ContentLink.ID}",
            ResourceType = ClientResourceType.Style,
            Path = _urlResolver.GetUrl((styleResource as IContent)?.ContentLink),
            IsMinified = styleResource.IsMinified,
            Attributes = GetAttributes(styleResource),
            Dependencies = GetDependencies(styleResource)
        };
    }

    // private method that sets the integrity attribute and others if available
    private static IDictionary<string, string> GetAttributes(IClientResourceConfiguration clientResource)
    {
        var attributes = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(clientResource.SubResourceIntegrity))
        {
            attributes.Add("integrity", clientResource.SubResourceIntegrity);
        }

        if (clientResource.Attributes != null)
        {
            foreach (var attribute in clientResource.Attributes)
            {
                if (string.IsNullOrWhiteSpace(attribute.Key) || string.IsNullOrWhiteSpace(attribute.Value))
                {
                    continue;
                }
                attributes.Add(attribute.Key, attribute.Value);
            }
        }

        return attributes;
    }

    private static List<string> GetDependencies(IClientResourceInclude clientResource)
    {
        // TODO: Future improvement: Can add a selection factory that surfaces other CDN scripts so dependencies can be configured via CMS.
        return new List<string>();
    }
}