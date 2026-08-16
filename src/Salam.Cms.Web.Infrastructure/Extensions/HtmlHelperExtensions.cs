namespace Salam.Cms.Web.Infrastructure.Extensions;

using EPiServer.Core;
using EPiServer.Web.Mvc.Html;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Linq.Expressions;

/// <summary>
/// A Collection of extension methods for the Html Helpers.
/// </summary>
public static class HtmlHelperExtensions
{
    /// <summary>
    /// Simplifies the creation of an MVC form that targets a content page.
    /// </summary>
    /// <typeparam name="T">The Model for the page</typeparam>
    /// <param name="htmlHelper">The generic html helper</param>
    /// <param name="contentReference">The Content Reference for the targeted content page</param>
    /// <param name="formMethod">The desired form method.</param>
    /// <param name="formClass">The css class to decorate the form.</param>
    /// <returns></returns>
    public static MvcForm BeginContentForm<T>(
        this IHtmlHelper<T> htmlHelper,
        ContentReference? contentReference,
        FormMethod formMethod,
        string formClass)
    {
        return htmlHelper.BeginContentForm(
            contentReference,
            CultureInfo.CurrentUICulture.Name,
            null,
            formMethod,
            false,
            new { @class = formClass });
    }

    /// <summary>
    /// Used for standard rendering of an <see cref="XhtmlString"/> of a rich text editor.
    /// This is used to pass down a class to be rendered on embedded blocks.
    /// This will need to be used in tandem with <see cref="GetRichTextContainerClass{T}"/> on the razor file for the embedded block.
    /// </summary>
    /// <typeparam name="T">The Model for the page</typeparam>
    /// <param name="htmlHelper">The generic html helper</param>
    /// <param name="richTextFunc">A function expression to target the <see cref="XhtmlString"/> on the Model</param>
    /// <returns></returns>
    public static IHtmlContent RenderRichTextContent<T>(this IHtmlHelper<T> htmlHelper, Expression<Func<T, XhtmlString?>> richTextFunc)
    {
        return htmlHelper.PropertyFor(richTextFunc, new { RichTextClass = "rte-block" });
    }

    /// <summary>
    /// Used for standard rendering of an <see cref="XhtmlString"/> of a rich text editor.
    /// This is used to pass down a class to be rendered on embedded blocks.
    /// This will need to be used in tandem with <see cref="GetRichTextContainerClass{T}"/> on the razor file for the embedded block.
    /// </summary>
    /// <typeparam name="T">The Model for the page</typeparam>
    /// <param name="htmlHelper">The generic html helper</param>
    /// <param name="richText">The <see cref="XhtmlString"/> on the Model</param>
    /// <returns></returns>
    public static IHtmlContent RenderRichTextContent<T>(this IHtmlHelper<T> htmlHelper, XhtmlString? richText)
    {
        return htmlHelper.PropertyFor(_ => richText, new { RichTextClass = "rte-block" });
    }

    /// <summary>
    /// Used for decorating a div of a block with a css class when a block is used within rich text content.
    /// This will need to be used in tandem with <see cref="M:RenderRichTextContent{T}"/> where the rich text will be rendered.
    /// </summary>
    /// <typeparam name="T">The Model for the page</typeparam>
    /// <param name="htmlHelper">The generic html helper</param>
    /// <returns></returns>
    public static string GetRichTextContainerClass<T>(this IHtmlHelper<T> htmlHelper)
    {
        return htmlHelper.ViewData["RichTextClass"]?.ToString() ?? string.Empty;
    }
}