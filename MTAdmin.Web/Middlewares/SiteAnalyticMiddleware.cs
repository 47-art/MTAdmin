using Microsoft.Extensions.Options;
using MTAdmin.Shared.Models;
using MTClient.Application.Services.Interfaces;
using System.Net;

namespace MTAdmin.Web.Middlewares
{
    public class SiteAnalyticMiddleware : IMiddleware
    {
        private readonly ISiteAnalyticsService _service;
        private List<Func<HttpContext, bool>> _exclude;
        private readonly SiteAnalyticsOptions _options;
        public SiteAnalyticMiddleware(ISiteAnalyticsService service,IOptions<SiteAnalyticsOptions> options)
        {
            _service = service;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!string.IsNullOrEmpty(_options.ExcludeExtension))
            {
                ExcludeExtension(_options.ExcludeExtension.Split(",").ToArray());
            }

            if (_options.ExcludeLoopBack)
            {
                ExcludeLoopBack();
            }

            if (!string.IsNullOrEmpty(_options.ExcludePaths))
            {
                ExcludePath(_options.ExcludePaths.Split(",").ToArray());
            }

            if (!string.IsNullOrEmpty(_options.ExcludeIPs))
            {
                string[] ips = _options.ExcludeIPs.Split(",").ToArray();
                IPAddress ip;
                for (global::System.Int32 i = 0; i < ips.Length; i++)
                {
                    if (IPAddress.TryParse(ips[i], out ip))
                    {
                        ExcludeIp(ip);
                    }
                }
            }

            if (_exclude?.Any(x => x(context)) ?? false)
            {
                await next(context);
                return;
            }
            else
            {
                var req = new SiteRequest
                {
                    StartTimestamp = DateTime.UtcNow,
                    Identity = context.UserIdentity(),
                    RemoteIpAddress = context.Connection.RemoteIpAddress,
                    Method = context.Request.Method,
                    UserAgent = context.Request.Headers["User-Agent"],
                    Path = context.Request.Path.Value,
                    IsWebSocket = context.WebSockets.IsWebSocketRequest,
                    CountryCode = 356
                };
                await next(context);
                req.EndTimestamp = DateTime.UtcNow;
                await _service.SaveSiteRequest(req);
            }
        }

        public SiteAnalyticMiddleware Exclude(Func<HttpContext, bool> filter)
        {
            if (_exclude == null) _exclude = new List<Func<HttpContext, bool>>();
            _exclude.Add(filter);
            return this;
        }

        public SiteAnalyticMiddleware Exclude(IPAddress ip) => Exclude(x => Equals(x.Connection.RemoteIpAddress, ip));

        public SiteAnalyticMiddleware LimitToPath(string path) => Exclude(x => !Equals(x.Request.Path.StartsWithSegments(path)));

        public SiteAnalyticMiddleware ExcludePath(params string[] paths)
        {
            return Exclude(x => paths.Any(path => x.Request.Path.StartsWithSegments(path)));
        }

        public SiteAnalyticMiddleware ExcludeExtension(params string[] extensions)
        {
            return Exclude(x => extensions.Any(ext => x.Request.Path.Value.EndsWith(ext)));
        }

        public SiteAnalyticMiddleware ExcludeLoopBack() => Exclude(x => IPAddress.IsLoopback(x.Connection.RemoteIpAddress));

        public SiteAnalyticMiddleware ExcludeIp(IPAddress address) => Exclude(x => x.Connection.RemoteIpAddress.Equals(address));        
    }
    public static class SiteAnalyticsExtensions
    {
        public static string UserIdentity(this HttpContext context)
        {
            //var user = context.User?.Identity?.Name;

            //return string.IsNullOrWhiteSpace(user)
            //    ? (context.Request.Cookies.ContainsKey("ai_user")
            //        ? context.Request.Cookies["ai_user"]
            //        : context.Connection.Id)
            //    : user;
            return context.Connection.Id;
        }
        public static IApplicationBuilder UseSiteAnalytics(this IApplicationBuilder builder)
        {            
            return builder.UseMiddleware<SiteAnalyticMiddleware>();
        }
    }
}
