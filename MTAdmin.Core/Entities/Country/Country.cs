namespace MTAdmin.Core.Entities.Country
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public int CountryCode { get; set; }
        public string TopLevelDomain { get; set; }
        public ICollection<GeoIpRange.GeoIpRange> GeoIpRanges { get; set; }
        public ICollection<SiteRequest.SiteRequest> SiteRequests { get; set; }
    }
}
