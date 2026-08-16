using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Framework;
using EPiServer.Find.Helpers.Text;
using EPiServer.Web.Routing;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.SearchCommentSanitizer;
public class SearchService
{
    private readonly UrlResolver _urlResolver;
    public SearchService(UrlResolver urlResolver)
    {
        _urlResolver = urlResolver;
    }
    public SearchResultsModel SearchContent(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return new SearchResultsModel();
        var contentResult = SearchClient.Instance.Search<B2BSitePageData>()
        .For(searchTerm).InFields(x=>x.Name,x=>x.Heading)
                        .InField(x=>x.MainContent)
                        .InField(x=>x.HeroArea)
                        .InField(x=>x.HTMLBody)
                        .InField(x=>x.EndPageMainContent)
                        .ExcludeDeleted()
                        .CurrentlyPublished()
        .FilterForVisitor().Select(x=>new  { Title =x.Name,
            HighlightedHero=x.HeroArea.AsHighlighted(),
            Heading=x.Heading.AsHighlighted(),
            MainContent=x.MainContent.AsHighlighted(
            new HighlightSpec
            {
                PreTag = "<strong>",
                PostTag = "</strong>",
                NumberOfFragments = 3,
                Concatenation = fragments => fragments.Concatenate(" ... ")
            }),
            EndContentArea=x.EndPageMainContent.AsHighlighted(
            new HighlightSpec
            {
                PreTag = "<strong>",
                PostTag = "</strong>",
                NumberOfFragments = 3,
                Concatenation = fragments => fragments.Concatenate(" ... ")
            }),
            HighlightedContent = x.HTMLBody.AsHighlighted(
            new HighlightSpec
            {
                PreTag = "<strong>",
                PostTag = "</strong>",
                NumberOfFragments = 3,
                Concatenation = fragments => fragments.Concatenate(" ... ")
            }),
             ContentLink = x.ContentLink // we need this to resolve URL later
        })
        .GetResultAsync();



        var hits = contentResult.Result?.Select(x => {

            var allHighlights = new List<string>();

            if (!string.IsNullOrEmpty(x.HighlightedHero))
                allHighlights.Add(x.HighlightedHero);

            if (!string.IsNullOrEmpty(x.Heading))
                allHighlights.Add(x.Heading);

            if (!string.IsNullOrEmpty(x.MainContent))
                allHighlights.Add(x.MainContent);

            if (!string.IsNullOrEmpty(x.EndContentArea))
                allHighlights.Add(x.EndContentArea);

            if (!string.IsNullOrEmpty(x.HighlightedContent))
                allHighlights.Add(x.HighlightedContent);

            var combinedSearchContent = allHighlights.Any() ? string.Join(" ... ", allHighlights) : "";

            // remove <!-- comments -->
            combinedSearchContent = SearchCommentSanitizer.RemoveHtmlComments(combinedSearchContent, searchTerm);
            // skip empty results after cleanup
            if (string.IsNullOrWhiteSpace(combinedSearchContent))
                return null;

            return new SearchResultItem
           {
               Content=combinedSearchContent,
               Title = x.Title,
               Url = x?.ContentLink != null
                    ? _urlResolver.GetUrl(x.ContentLink)
                    : null,
            };
       }).Where(x => x != null)
         .ToList();
       return new SearchResultsModel
       {
           Results = hits
       };
    }
}
public class SearchResultsModel
{
    public List<SearchResultItem> Results
    {
        get;
        set;
    } 
}
public class SearchResultItem
{
    
    public ContentReference ContentLink
    {
        get;
        set;
    }
    public string Title
    {
        get;
        set;
    }
    public string Url
    {
        get;
        set;
    }
    public string Content
    {
        get;
        set;
    }
}

