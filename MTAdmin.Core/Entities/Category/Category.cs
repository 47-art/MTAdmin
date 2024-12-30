using MTAdmin.Core.Entities.Comman;

namespace MTAdmin.Core.Entities.Category
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Slug { get; set; }
        public int Views { get; set; }
        public string Img { get; set; }
        public string ImgAlt { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeywords { get; set; }
        public ICollection<TemplateCategory.TemplateCategory> TemplateCategories { get; set; }
    }
}
