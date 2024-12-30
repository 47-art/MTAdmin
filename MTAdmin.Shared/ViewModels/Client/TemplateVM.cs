using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Models;
using System;

namespace MTAdmin.Shared.ViewModels.Client
{
    public abstract class Template
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Thumbnail { get; set; }
        public string ThumbnailAlt { get; set; }
        public string FileName { get; set; }
        public string CategorySlug { get; set; }
        public int Popularity { get; set; }
        public DateTime CreatedAt { get; set; }
        public TemplateTypeEnum TemplateType { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeywords { get; set; }
        public string CategoryMetaDesc { get; set; }
        public string CategoryMetaKeywords { get; set; }
    }
    public class TemplateVM : Template
    {
                           
    }
    public class ListTemplateVM
    {
        public string CategorySlug { get; set; }
        public int CategoryId { get; set; }
        public FilterByEnum FilterById { get; set; }
        public PagedList<TemplateVM> TemplateList { get; set; }
    }
    public class TemplateParams : QueryStringParameters
    {
        public int CategoryId { get; set; }
        public FilterByEnum FilterBy { get; set; } = FilterByEnum.latest;
    }
    public class ShareTemplateVM
    {
        public int Id { get; set; }
        public PostEventTypeEnum ShareType { get; set; }
    }
}
