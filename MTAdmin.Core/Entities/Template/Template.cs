using MTAdmin.Core.Entities.Comman;

namespace MTAdmin.Core.Entities.Template
{
    public class Template : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Desc { get; set; }
        public int Views { get; set; }
        public int LanguageId { get; set; }
        public Language.Language Language { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeywords { get; set; }
        public string Thumbnail { get; set; }
        public string ThumbnailAlt { get; set; }
        public string FileName { get; set; }
        public ICollection<TemplateCategory.TemplateCategory> TemplateCategories { get; set; }
        public ICollection<TemplateTag.TemplateTag> TemplateTags { get; set; }
        //public ICollection<TemplateMedia.TemplateMedia> TemplateMedias { get; set; }
    }
}
