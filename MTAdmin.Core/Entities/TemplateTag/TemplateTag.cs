namespace MTAdmin.Core.Entities.TemplateTag
{
    public class TemplateTag
    {
        public int TemplateId { get; set; }
        public Template.Template Template { get; set; }
        public int TagId { get; set; }
        public Tag.Tag Tag { get; set; }
    }
}
