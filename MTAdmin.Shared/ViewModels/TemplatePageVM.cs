using Microsoft.AspNetCore.Http;
using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MTAdmin.Shared.ViewModels
{
    public abstract class TemplateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        public string Desc { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [Required]
        public List<int> CategoryIds { get; set; }
        public List<int> TagIds { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeywords { get; set; }
        public bool IsActive { get; set; }
        public string Thumbnail { get; set; }
        public string FileName { get; set; }
    }
    public class CreateTemplateVM : TemplateVM
    {
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile ThumbnailFile { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile TemplateFile { get; set; }
        public string ThumbnailAlt { get; set; }
    }
    public class EditTemplateVM : TemplateVM
    {
        public int Id { get; set; }                
        public string Categories { get; set; }
        public string Tags { get; set; }
    }
    public class TemplatePageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public long Views { get; set; }
        public bool IsActive { get; set; }
        public float ViewsInPer { get; set; }
    }    
    public class TemplateParameters : QueryStringParameters
    {

    }
    public class EditTemplateThumbnailVM
    {
        public int Id { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile ThumbnailFile { get; set; }
        public string Thumbnail { get; set; }
        public string ThumbnailAlt { get; set; }
    }
    public class EditTemplateFileVM
    {
        public int Id { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile TemplateFile { get; set; }
        public string FileName { get; set; }
        public TemplateTypeEnum TemplateType { get; set; }
    }        
}
