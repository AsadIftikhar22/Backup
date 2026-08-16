namespace Salam.Cms.Web.Features.Common.Components.TagManager;

using Newtonsoft.Json;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public sealed class DataLayer
{
    /// <summary>
    /// Gets or sets the Site name as used in the page title
    /// </summary>
    [JsonProperty("site")]
    public string? SiteName { get; set; }

    /// <summary>
    /// Gets or sets the Page title
    /// </summary>
    [JsonProperty("title")]
    public string? PageTitle { get; set; }

    /// <summary>
    /// Gets or sets the relative url
    /// </summary>
    [JsonProperty("url")]
    public string? RelativeUrl { get; set; }

    /// <summary>
    /// Gets or sets the absolute canonical url of the page
    /// </summary>
    [JsonProperty("urlAbs")]
    public string? AbsoluteUrl { get; set; }

    /// <summary>
    /// Gets or sets the page content type id
    /// </summary>
    [JsonProperty("pID")]
    public string? PageContentTypeId { get; set; }

    /// <summary>
    /// Gets or sets the Page classification: 'Home', 'Article', 'Listing'
    /// </summary>
    [JsonProperty("pClass")]
    public string? PageClassification { get; set; }

    /// <summary>
    /// Gets or sets the page language in ISO format: 'en' / 'en-gb'
    /// </summary>
    [JsonProperty("pLang")]
    public string? PageLanguage { get; set; }

    /// <summary>
    /// Gets or sets the page parent content ID
    /// </summary>
    [JsonProperty("pPar")]
    public string? PageParentId { get; set; }

    /// <summary>
    /// Gets or sets the Page ancestors id array: [1,2,3]
    /// </summary>
    [JsonProperty("pAnc")]
    public string? PageAncestors { get; set; }

    /// <summary>
    /// Gets or sets the Site host name without the protocol: www.domain.com
    /// </summary>
    [JsonProperty("Host")]
    public string? HostName { get; set; }

    /// <summary>
    /// Gets or sets the full absolute URI for the request
    /// </summary>
    [JsonProperty("Uri")]
    public string? Uri { get; set; }

    /// <summary>
    /// Gets or sets the User state: 'Signed in' / 'Signed out'
    /// </summary>
    [JsonProperty("uState")]
    public string? UserLoggedInState { get; set; }

    /// <summary>
    /// Gets or sets the user's group / permission level: 'WebAdmin', 'WebEditor', 'User' or 'none' if none applies / if the user is logged out
    /// </summary>
    [JsonProperty("uLvl")]
    public string? UserAccessLevel { get; set; }

    /// <summary>
    /// Gets or sets the Unix / epoch time
    /// </summary>
    [JsonProperty("Time")]
    public string? RequestedTime { get; set; }
}