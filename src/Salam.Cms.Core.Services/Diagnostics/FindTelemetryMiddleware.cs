//using System.Threading;
//using System.Threading.Tasks;
//using EPiServer.Find;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;

//namespace Salam.Cms.Core.Services.Diagnostics;

///// <summary>
///// Middleware that logs, per HTTP request, how many Optimizely Find searches were executed
///// and how many documents they returned in total. Uses the built-in Diagnostics event so we
///// do not need to proxy IClient.
///// </summary>
//public sealed class FindTelemetryMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly ILogger<FindTelemetryMiddleware> _logger;
//    private static readonly AsyncLocal<RequestStats?> _stats = new();
//    private static bool _hooked;

//    public FindTelemetryMiddleware(RequestDelegate next, ILogger<FindTelemetryMiddleware> logger, IClient client)
//    {
//        _next = next;
//        _logger = logger;

//        // global one-time hook
//        if (!_hooked)
//        {
//            client.Diagnostics.RequestCompleted += OnRequestCompleted;
//            _hooked = true;
//        }
//    }

//    private static void OnRequestCompleted(object? sender, RequestCompletedEventArgs e)
//    {
//        var s = _stats.Value;
//        if (s == null) return; // not inside an HTTP request
//        s.Calls++;
//        s.TotalHits += e.Result?.Hits?.Count ?? 0;
//    }

//    public async Task Invoke(HttpContext context)
//    {
//        _stats.Value = new RequestStats();
//        await _next(context);

//#if DEBUG
//        var s = _stats.Value;
//        if (s != null && s.Calls > 0)
//        {
//            _logger.LogInformation("Find telemetry: {Calls} searches, {Hits} hits, Path={Path}", s.Calls, s.TotalHits, context.Request.Path);
//        }
//#endif
//        _stats.Value = null; // reset
//    }

//    private sealed class RequestStats
//    {
//        public int Calls;
//        public int TotalHits;
//    }
//} 