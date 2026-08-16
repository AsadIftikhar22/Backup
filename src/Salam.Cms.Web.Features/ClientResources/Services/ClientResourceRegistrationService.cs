namespace Salam.Cms.Web.Features.ClientResources.Services;

using EPiServer.Core;
using EPiServer.Framework.Web.Resources;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Salam.Cms.Web.Features.ClientResources.Abstract;
using Salam.Cms.Web.Features.ClientResources.Common;
using Salam.Cms.Web.Features.ClientResources.Enums;
using System;
using System.Collections.Generic;

public class ClientResourceRegistrationService : IClientResourceRegistrationService
{
    private readonly IRequiredClientResourceList _requiredClientResourceList;
    private readonly IContextModeResolver _contextModeResolver;
    private readonly IUrlResolver _urlResolver;

    public ClientResourceRegistrationService(IRequiredClientResourceList requiredClientResourceList,
                                             IContextModeResolver contextModeResolver,
                                             IUrlResolver urlResolver)
    {
        _requiredClientResourceList = requiredClientResourceList;
        _contextModeResolver = contextModeResolver;
        _urlResolver = urlResolver;
    }

    public void RegisterEmbed(IEmbedResourceInclude embedResource)
    {
        var contentReference = ((IContent)embedResource).ContentLink.ID;

        var preferredRenderLocation = embedResource.RenderLocation;

        if (preferredRenderLocation == EmbedRenderLocationOption.Head)
        {
            _ = _requiredClientResourceList.Require($"{ClientResourceConstants.RegisteredResourcePrefix}{contentReference}").AtHeader();
        }
        else if (preferredRenderLocation == EmbedRenderLocationOption.BodyStart)
        {
            _ = _requiredClientResourceList.Require($"{ClientResourceConstants.RegisteredResourcePrefix}{contentReference}").AtArea(ClientResourceConstants.BodyStart);
        }
        else if (preferredRenderLocation == EmbedRenderLocationOption.BodyEnd)
        {
            _ = _requiredClientResourceList.Require($"{ClientResourceConstants.RegisteredResourcePrefix}{contentReference}").AtFooter();
        }
        else
        {
            _ = _requiredClientResourceList.Require($"{ClientResourceConstants.RegisteredResourcePrefix}{contentReference}");
        }
    }

    public void RegisterEmbeds(IEnumerable<IEmbedResourceInclude?>? embedResources)
    {
        // null check
        if (embedResources == null)
        {
            return;
        }

        foreach (var resource in embedResources)
        {
            // null check
            if (resource == null)
            {
                continue;
            }

            if (!resource.IsLoadedInEditMode && _contextModeResolver.CurrentMode.EditOrPreview())
            {
                continue;
            }

            RegisterEmbed(resource);
        }
    }

    public void RegisterExternalResource(IExternalResourceInclude externalResource)
    {
        var contentReference = ((IContent)externalResource).ContentLink.ID;

        var preferredRenderLocation = externalResource.RenderLocation;

        if (preferredRenderLocation == ClientResourceRenderLocationOption.Head)
        {
            _ = _requiredClientResourceList.Require($"{ClientResourceConstants.RegisteredResourcePrefix}{contentReference}").AtHeader();
        }
        else if (preferredRenderLocation == ClientResourceRenderLocationOption.BodyStart)
        {
            _ = _requiredClientResourceList.Require($"{ClientResourceConstants.RegisteredResourcePrefix}{contentReference}").AtArea(ClientResourceConstants.BodyStart);
        }
        else if (preferredRenderLocation == ClientResourceRenderLocationOption.BodyEnd)
        {
            _ = _requiredClientResourceList.Require($"{ClientResourceConstants.RegisteredResourcePrefix}{contentReference}").AtFooter();
        }
        else
        {
            _ = _requiredClientResourceList.Require($"{ClientResourceConstants.RegisteredResourcePrefix}{contentReference}");
        }
    }

    public void RegisterExternalResources(IEnumerable<IExternalResourceInclude?>? externalResources)
    {
        // null check
        if (externalResources == null)
        {
            return;
        }

        foreach (var resource in externalResources)
        {
            // null check
            if (resource == null)
            {
                continue;
            }

            if (!resource.IsLoadedInEditMode && _contextModeResolver.CurrentMode.EditOrPreview())
            {
                continue;
            }

            RegisterExternalResource(resource);
        }
    }

    public void RegisterResource(IClientResourceInclude clientResource)
    {
        switch (clientResource)
        {
            case IExternalResourceInclude externalResource:
                RegisterExternalResource(externalResource);
                break;
            case IEmbedResourceInclude embedResource:
                RegisterEmbed(embedResource);
                break;
            case IScriptResourceInclude scriptResource:
                RegisterScript(scriptResource);
                break;
            case IStyleResourceInclude styleResource:
                RegisterStyle(styleResource);
                break;
            default:
                throw new NotSupportedException($"Resource Type {clientResource.GetType().Name} is not supported.");
        }
    }

    public void RegisterResources(IEnumerable<IClientResourceInclude?>? clientResources)
    {
        // null check
        if (clientResources == null)
        {
            return;
        }

        foreach (var resource in clientResources)
        {
            // null check
            if (resource == null)
            {
                continue;
            }

            if (!resource.IsLoadedInEditMode && _contextModeResolver.CurrentMode.EditOrPreview())
            {
                continue;
            }

            RegisterResource(resource);
        }
    }

    public void RegisterScript(IScriptResourceInclude scriptResource)
    {
        if (!scriptResource.IsLoadedInEditMode && _contextModeResolver.CurrentMode.EditOrPreview())
        {
            return;
        }

        var contentLink = ((IContent)scriptResource).ContentLink;

        var preferredRenderLocation = scriptResource.RenderLocation;

        if (preferredRenderLocation == ClientResourceRenderLocationOption.Head)
        {
            _ = _requiredClientResourceList.RequireScript(_urlResolver.GetUrl(contentLink)).AtHeader();
        }
        else if (preferredRenderLocation == ClientResourceRenderLocationOption.BodyStart)
        {
            _ = _requiredClientResourceList.RequireScript(_urlResolver.GetUrl(contentLink)).AtArea(ClientResourceConstants.BodyStart);
        }
        else if (preferredRenderLocation == ClientResourceRenderLocationOption.BodyEnd)
        {
            _ = _requiredClientResourceList.RequireScript(_urlResolver.GetUrl(contentLink)).AtFooter();
        }
        else
        {
            _ = _requiredClientResourceList.RequireScript(_urlResolver.GetUrl(contentLink));
        }
    }

    public void RegisterScripts(IEnumerable<IScriptResourceInclude?>? scriptResources)
    {
        // null check
        if (scriptResources == null)
        {
            return;
        }

        foreach (var script in scriptResources)
        {
            // null check
            if (script == null)
            {
                continue;
            }

            if (!script.IsLoadedInEditMode && _contextModeResolver.CurrentMode.EditOrPreview())
            {
                continue;
            }

            RegisterScript(script);
        }
    }

    public void RegisterStyle(IStyleResourceInclude styleResource)
    {
        if (!styleResource.IsLoadedInEditMode && _contextModeResolver.CurrentMode.EditOrPreview())
        {
            return;
        }

        var contentLink = ((IContent)styleResource).ContentLink;

        var preferredRenderLocation = styleResource.RenderLocation;

        if (preferredRenderLocation == ClientResourceRenderLocationOption.Head)
        {
            _ = _requiredClientResourceList.RequireStyle(_urlResolver.GetUrl(contentLink)).AtHeader();
        }
        else if (preferredRenderLocation == ClientResourceRenderLocationOption.BodyStart)
        {
            _ = _requiredClientResourceList.RequireStyle(_urlResolver.GetUrl(contentLink)).AtArea(ClientResourceConstants.BodyStart);
        }
        else if (preferredRenderLocation == ClientResourceRenderLocationOption.BodyEnd)
        {
            _ = _requiredClientResourceList.RequireStyle(_urlResolver.GetUrl(contentLink)).AtFooter();
        }
        else
        {
            _ = _requiredClientResourceList.RequireStyle(_urlResolver.GetUrl(contentLink));
        }
    }

    public void RegisterStyles(IEnumerable<IStyleResourceInclude?>? styleResources)
    {
        // null check
        if (styleResources == null)
        {
            return;
        }

        foreach (var style in styleResources)
        {
            // null check
            if (style == null)
            {
                continue;
            }

            if (!style.IsLoadedInEditMode && _contextModeResolver.CurrentMode.EditOrPreview())
            {
                continue;
            }

            RegisterStyle(style);
        }
    }
}
