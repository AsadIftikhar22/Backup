namespace Salam.Cms.Core.Settings.Abstract;

using EPiServer.Core;

using System.Globalization;

public interface ISettingsManager
{
    T GetSettings<T>(CultureInfo culture = null) where T : IContentData;
}
