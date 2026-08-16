namespace Salam.Cms.Web.Features.Common.Helpers.Tags;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using System;

public static class FormDisplayOptionTags
{
    public static readonly string FullWidth = "full-width";
    public static readonly string HalfWidth = "half-width";
}
public static class ContentAreaTags
{
    public const string FullWidth = "full";
    public const string HalfWidth = "half";
}

[InitializableModule]
[ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
public class FormDisplayOptionsInitialization : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        var displayOptions = context.Locate.Advanced.GetInstance<DisplayOptions>();
        displayOptions
            .Add("full-width", "/displayoptions/full-width", "full-width", FormDisplayOptionTags.FullWidth)
            .Add("half-width", "/displayoptions/full-width", "half-width", FormDisplayOptionTags.HalfWidth);
    }

    public void Uninitialize(InitializationEngine context) { }
}