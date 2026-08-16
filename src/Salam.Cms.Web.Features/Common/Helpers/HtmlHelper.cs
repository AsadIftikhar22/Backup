// Helpers/HtmlHelpers.cs
using EPiServer.Core;
using EPiServer.Data.Entity;
using EPiServer.Forms.Core;
using EPiServer.Forms.Core.Models;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Web.Features.ClientResources.Services;
using Salam.Cms.Web.Features.Common.Helpers.Rendering;

public static class HtmlHelpers
{
    public static IHtmlContent RenderCssInline(this IHtmlHelper htmlHelper, string pattern)
    {
        var loader = htmlHelper.ViewContext.HttpContext.RequestServices.GetService<IInlineCssService>();
        return loader?.LoadInlineCss(pattern) ?? HtmlString.Empty;
    }

    public static void RenderForm(this IHtmlHelper html, int currentStepIndex, IEnumerable<IFormElement> elements)
    {
        var _formContentAreaRender = ServiceLocator.Current.GetInstance<FormContentAreaRender>();
        FormContainerBlock model = (FormContainerBlock)html.ViewData.Model;
        if (model == null)
            return;

        string columnWidth = string.Empty;
        var ContentAreaElements = elements.Select(element =>
                    model.ElementsArea.Items.FirstOrDefault(i => i.ContentLink == element.SourceContent.ContentLink));

        foreach (var formelement in ContentAreaElements)
        {

                //check if row two fields in line have the half width than add those two fields in separate div two by two fields
                IContent content = formelement.GetContent();
                if (content == null || content.IsDeleted)
                {
                    continue;
                }
                var cssClasses = _formContentAreaRender.GetItemCssClass((HtmlHelper)html, formelement);
                html.ViewContext.Writer.Write($"<div class=\"{cssClasses}\">");

                if (content is ISubmissionAwareElement)
                {
                    var submissionAwareElement = (content as IReadOnly).CreateWritableClone() as IContent;
                    (submissionAwareElement as ISubmissionAwareElement).FormSubmissionId = html.ViewBag.FormSubmissionId;
                    html.RenderContentData(submissionAwareElement, false);
                }
                else
                {
                    html.RenderContentData(content, false);
                }
                html.ViewContext.Writer.Write("</div>");
        }
    }
}
