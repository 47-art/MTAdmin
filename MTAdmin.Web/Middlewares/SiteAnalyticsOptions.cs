using System.Net;

namespace MTAdmin.Web.Middlewares
{
    public class SiteAnalyticsOptions
    {
        public string ExcludeIPs { get; set; }
        public string ExcludeExtension { get; set; }
        public string ExcludePaths { get; set; }
        public bool ExcludeLoopBack { get; set; }
    }
}
