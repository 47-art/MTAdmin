using MTAdmin.Core.Entities.Comman;

namespace MTAdmin.Core.Entities.PageMetaTags
{
    public class PageMetaTag : BaseEntity<int>
    {
        public int PageId { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeywords { get; set; }
    }
}
