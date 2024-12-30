namespace MTAdmin.Core.Entities.SiteRequest
{
    public class SiteRequest
    {
        public long Id { get; set; }
        public DateTime StartTimestamp { get; set; }
        public DateTime EndTimestamp { get; set; }
        public string Identity { get; set; }
        public string RemoteIpAddress { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string UserAgent { get; set; }
        public string Referer { get; set; }
        public bool IsWebSocket { get; set; }
        public int CountryId { get; set; }
        public Country.Country Country { get; set; }
    }
}
