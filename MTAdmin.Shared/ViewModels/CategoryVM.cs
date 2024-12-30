using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
using MTAdmin.Shared.Models;

namespace MTAdmin.Shared.ViewModels
{
    public class CreateCategoryVM
    {
        [Required]
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Slug { get; set; }
        public string Img { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile ImgFile { get; set; }
        [Required]
        [MaxLength(100)]
        public string ImgAlt { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeywords { get; set; }
        public bool IsActive { get; set; }
    }
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Slug { get; set; }
        public string Img { get; set; }
        [Required]
        [MaxLength(100)]
        public string ImgAlt { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeywords { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
    public class CategoryPageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int TemplateCount { get; set; }
        public float TemplateUsage { get; set; }
    }

    public class CategoryParameters : QueryStringParameters
    {

    }

    public class EditCategoryImgVM
    {
        public int Id { get; set; }
        [Required]
        public IFormFile Img { get; set; }
    }
}
