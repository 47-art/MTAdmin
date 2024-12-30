using Microsoft.Extensions.Options;
using MTAdmin.Web.Middlewares;
using MTClient.Application.Services.Interfaces;
using System.Net;

namespace MTAdmin.Web.Services
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly HttpContext context;
        private readonly SiteAnalyticsOptions _options;
        private List<Func<HttpContext, bool>> _exclude;
        public HttpContextService(IHttpContextAccessor accessor, IOptions<SiteAnalyticsOptions> options)
        {
            _accessor = accessor;
            _options = options.Value;
            if (_accessor.HttpContext is not null)
            {
                context = _accessor.HttpContext;
            }
        }
        public string GetIPAddress()
        {
            string ipstr = context.Connection.RemoteIpAddress.ToString();

            if (_options.ExcludeLoopBack)
            {
                ExcludeLoopBack();
            }

            if (!string.IsNullOrEmpty(_options.ExcludeIPs) && !string.IsNullOrEmpty(ipstr))
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
                return null;
            }
            else
            {
                return ipstr;
            }
        }
        public string GetConnectionId()
        {
            return context.Connection.Id;
        }
        public (string connectionId, string ip) GetConnectionIdAndIP()
        {
            return (GetIPAddress(), GetConnectionId());
        }
        private HttpContextService ExcludeIp(IPAddress address) => Exclude(x => x.Connection.RemoteIpAddress.Equals(address));
        public HttpContextService ExcludeLoopBack() => Exclude(x => IPAddress.IsLoopback(x.Connection.RemoteIpAddress));
        private HttpContextService Exclude(Func<HttpContext, bool> filter)
        {
            if (_exclude == null) _exclude = new List<Func<HttpContext, bool>>();
            _exclude.Add(filter);
            return this;
        }
    }
}
