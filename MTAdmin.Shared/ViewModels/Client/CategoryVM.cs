using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Models;

namespace MTAdmin.Shared.ViewModels.Client
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Img { get; set; }
        public string ImgAlt { get; set; }
        public int Views { get; set; }
        public int TemplateCount { get; set; }
        public float TemplateUsage { get; set; }
    }
    public class ListCategoryVM
    {
        public CategoryFilterEnum FilterBy { get; set; }
        public PagedList<CategoryVM> CategoryList { get; set; }
    }
    public class CategoryParams : QueryStringParameters
    {
        public CategoryFilterEnum FilterBy { get; set; } = CategoryFilterEnum.all;
    }
}
