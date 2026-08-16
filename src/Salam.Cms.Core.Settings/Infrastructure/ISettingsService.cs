namespace Salam.Cms.Core.Settings.Infrastructure;

using EPiServer.Core;
using Salam.Cms.Core.Settings.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

public interface ISettingsService
{
    ContentReference? GlobalSettingsRoot { get; set; }
    ConcurrentDictionary<string, Dictionary<Type, SettingsBase>> SiteSettings { get; }
    T? GetSiteSettings<T>(Guid? siteId = null) where T : SettingsBase;
    Dictionary<Type, SettingsBase>? GetSiteSettings(Guid? siteId = null);
    void InitializeSettings();
    void UnintializeSettings();
    void UpdateSettings(Guid siteId, SettingsBase content, bool isContentNotPublished);
    void UpdateSettings();
}
