namespace MTAdmin.Core.Entities.GeoIpRange
{
    public class GeoIpRange
    {
        public long Id { get; set; }
        public long FromUp { get; set; }
        public long FromDown { get; set; }

        public long ToUp { get; set; }
        public long ToDown { get; set; }

        public int CountryId { get; set; }
        public Country.Country Country { get; set; }
    }
}
