using MTAdmin.Core.Entities.Comman;

namespace MTAdmin.Core.Entities.Tag
{
    public class Tag : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public ICollection<TemplateTag.TemplateTag> TemplateTags { get; set; }
    }
}
