using System.Net;
using System;

namespace MTAdmin.Shared.Models
{
    public class SiteRequest
    {
        public DateTime StartTimestamp { get; set; }
        public DateTime EndTimestamp { get; set; }
        public string Identity { get; set; }

        public IPAddress RemoteIpAddress { get; set; }

        public string Method { get; set; }

        public string Path { get; set; }

        public string UserAgent { get; set; }

        public string Referer { get; set; }

        public bool IsWebSocket { get; set; }

        public int CountryCode { get; set; }
    }
}
