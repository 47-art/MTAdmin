using System.Collections.Generic;

namespace MTAdmin.Shared.Models
{
    public class CountryModel
    {
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public int CountryCode { get; set; }
        public string TopLevelDomain { get; set; }
    }
    public class CountryList
    {
        public IReadOnlyCollection<CountryModel> Countries { get; set; }
    }
}
