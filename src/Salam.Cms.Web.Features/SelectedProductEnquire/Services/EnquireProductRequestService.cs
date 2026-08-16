namespace Salam.Cms.Web.Features.SelectedProductEnquire.Services;

using EPiServer.Core;
using EPiServer.Core.Internal;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Http;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.EnquireProduct.Services;
using Salam.Cms.Web.Features.InternetCards.Models;
using Salam.Cms.Web.Features.SelectedProductEnquire.Models;
using Salam.Cms.Web.Features.Settings.Models;
using System.Globalization;

public class EnquireProductRequestService
{
    private readonly ISessionService _sessionService;
    private readonly ContentLoader _contentLoader;
    private readonly ISettingsManager _settingsManager;
    private readonly UrlResolver _uRLResolver;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public EnquireProductRequestService(ISessionService sessionService,
                                        ContentLoader contentLoader,
                                        ISettingsManager settingsManager,
                                        UrlResolver uRLResolver,
                                        IHttpContextAccessor httpContextAccessor)
    {
        _sessionService = sessionService;
        _contentLoader = contentLoader;
        _settingsManager = settingsManager;
        _uRLResolver = uRLResolver;
        _httpContextAccessor=httpContextAccessor;
    }
    public ResultStatus RemoveProductFromSession(int blockId)
    {
        ResultStatus obj_ResultStatus = new ResultStatus();
        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();
        List<EnquireProductRequest> lst_EnquireProductRequest = new List<EnquireProductRequest>();
        try
        {
            var sessionId = _httpContextAccessor.HttpContext!.Session.Id;
            var list = _sessionService.GetObject<List<EnquireProductRequest>>(sessionId) ?? new List<EnquireProductRequest>();
            list.RemoveAll(x => x.blockId == blockId);
            _sessionService.SetObject(sessionId, list);
            obj_ResultStatus.ResponseMessage = webLayoutSettings?.RemoveProductEnquireMessage ?? "Product Remove Successfully";
            obj_ResultStatus.ResponseStatus = (int)ResponseStatus.Success;
        }
        catch (Exception ex)
        {
            string msg = $"Error is {ex.StackTrace} and Message is {ex.Message}";
            Console.WriteLine(msg);
            obj_ResultStatus.ResponseMessage = msg;
            obj_ResultStatus.ResponseStatus = (int)ResponseStatus.Error;
        }
        return obj_ResultStatus;
    }

    public void RemoveAllProductsForSpecificUser(string Key)
    {
        ResultStatus obj_ResultStatus = new ResultStatus();
        WebLayoutSettings obj_webLayoutSettings = new();

        obj_webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        List<EnquireProductRequest> lst_EnquireProductRequest = new List<EnquireProductRequest>();
        try
        {
            var list = _sessionService.GetObject<List<EnquireProductRequest>>(Key) ?? new List<EnquireProductRequest>();
            list.Clear();
            _sessionService.SetObject(Key, list);
        }
        catch (Exception ex)
        {
            string msg = $"Error is {ex.StackTrace} and Message is {ex.Message}";
            Console.WriteLine(msg);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request_EnquireProductRequest"></param>
    /// <param name="Key"></param>
    /// <returns></returns>
    public ResultStatus SaveSelectedProductInSession(EnquireProductRequest request_EnquireProductRequest)
    {
        ResultStatus obj_ResultStatus = new ResultStatus();
        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>(CultureInfo.GetCultureInfo(request_EnquireProductRequest.language));
        try
        {
            var sessionId = _httpContextAccessor.HttpContext!.Session.Id;
            List<EnquireProductRequest> lst_EnquireProductRequest = new List<EnquireProductRequest>();
            var list = _sessionService.GetObject<List<EnquireProductRequest>>(sessionId) ?? new List<EnquireProductRequest>();
            request_EnquireProductRequest.id = list.Count + 1;

            //if (!String.IsNullOrWhiteSpace(request_EnquireProductRequest.redirectchildpageURL))
            //{
            //    Guid guid = PermanentLinkUtility.GetGuid(request_EnquireProductRequest.redirectchildpageURL);
            //    if (guid != Guid.Empty)
            //    {
            //        var redirectchildpage = _contentLoader.Get<B2BSitePageData>(guid);
            //        if (redirectchildpage != null)
            //            request_EnquireProductRequest.PageContentLink = redirectchildpage.ContentLink.ID;
            //    }
            //}
            string RedirectPageSuccessContentLink = string.Empty;
            if (!String.IsNullOrWhiteSpace(webLayoutSettings?.RedirectProductEnquirePageURL?.Href))
            {
                Guid guid = PermanentLinkUtility.GetGuid(webLayoutSettings?.RedirectProductEnquirePageURL?.Href);
                if (guid != Guid.Empty)
                {
                    var redirectchildpage = _contentLoader.Get<B2BSitePageData>(guid);
                    if (redirectchildpage != null)
                        RedirectPageSuccessContentLink = _uRLResolver.GetUrl(redirectchildpage.ContentLink, request_EnquireProductRequest.language);
                }
            }

            if (list.Count == webLayoutSettings?.MaxProductEnquireLimit)
            {
                obj_ResultStatus.ResponseMessage = webLayoutSettings?.ProductLimitMessage ?? "Not more than 10 Products can be added";
                obj_ResultStatus.ResponseStatus = (int)ResponseStatus.ProductLimitExceeds;
                obj_ResultStatus.RedirectSelectedProductPageURL = RedirectPageSuccessContentLink + ($"?success={obj_ResultStatus.ResponseMessage}");
                return obj_ResultStatus;
            }

            if (list.Any(x => x.blockId == request_EnquireProductRequest?.blockId))
            {
                obj_ResultStatus.ResponseMessage = webLayoutSettings?.ProductAlreadyExistMessage ?? "Product Already added";
                obj_ResultStatus.ResponseStatus = (int)ResponseStatus.ProductAlreadyExists;
                obj_ResultStatus.RedirectSelectedProductPageURL = RedirectPageSuccessContentLink + ($"?success={obj_ResultStatus.ResponseMessage}");
                return obj_ResultStatus;
            }
            //if (list.Any(x =>
            //         x.PageContentLink == request_EnquireProductRequest?.PageContentLink
            //    && (
            //             (!string.IsNullOrEmpty(x.enTabName)
            //                 && x.enTabName == request_EnquireProductRequest?.enTabName)
            //             ||
            //             (!string.IsNullOrEmpty(x.arTabName)
            //                 && x.arTabName == request_EnquireProductRequest?.arTabName)
            //         )
            //     ) || (list.Any(x =>
            //         x.PageContentLink == request_EnquireProductRequest?.PageContentLink && string.IsNullOrEmpty(x.enTabName) && string.IsNullOrEmpty(x.arTabName))))
            //{
            //    //obj_ResultStatus.ResponseMessage = webLayoutSettings?.ProductAlreadyExistMessage ?? "Product Already added";
            //    //obj_ResultStatus.ResponseStatus = (int)ResponseStatus.ProductAlreadyExists;
            //    obj_ResultStatus.RedirectSelectedProductPageURL = RedirectPageSuccessContentLink + ($"?success={obj_ResultStatus.ResponseMessage}");
            //    return obj_ResultStatus;
            //}
            list.Add(request_EnquireProductRequest);
            _sessionService.SetObject(sessionId, list);
            obj_ResultStatus.ResponseMessage = webLayoutSettings?.SaveProductEnquireMessage ?? "Product Added Successfully";
            obj_ResultStatus.ResponseStatus = (int)ResponseStatus.Success;
            obj_ResultStatus.RedirectSelectedProductPageURL = RedirectPageSuccessContentLink + ($"?success={obj_ResultStatus.ResponseMessage}");
            Console.WriteLine($"Redirect Selected Product Page URL,{obj_ResultStatus.RedirectSelectedProductPageURL}");
        }
        catch (Exception ex)
        {
            string msg = $"Error is {ex.StackTrace} and Message is {ex.Message}";
            Console.WriteLine(msg);
            obj_ResultStatus.ResponseMessage = msg;
            obj_ResultStatus.ResponseStatus = (int)ResponseStatus.Error;
        }
        return obj_ResultStatus;
    }
}
public enum ResponseStatus
{
    Success,
    Error,
    Warning,
    ProductAlreadyExists,
    ProductRemoved,
    ProductLimitExceeds,
    ProductAddedSuccessfully
}
