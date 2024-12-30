using MTAdmin.Core.Entities.Comman;

namespace MTAdmin.Core.Entities.Language
{
    public class Language : BaseEntity<int>
    {
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string Slug { get; set; }
        public ICollection<Template.Template> Templates { get; set; }
    }
}
