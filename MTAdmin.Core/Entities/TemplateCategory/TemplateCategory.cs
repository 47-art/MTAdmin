namespace MTAdmin.Core.Entities.TemplateCategory
{
    public class TemplateCategory
    {
        public int TemplateId { get; set; }
        public Template.Template Template { get; set; }
        public int CategoryId { get; set; }
        public Category.Category Category { get; set; }
    }
}
