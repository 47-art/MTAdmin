using MTAdmin.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace MTAdmin.Shared.ViewModels
{
    public class CreateTagVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsActive { get; set; }
    }
    public class TagVM : CreateTagVM
    {
        public int Id { get; set; }     
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

    public class TagPageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int TemplateCount { get; set; }
        public float TemplateUsage { get; set; }
    }
    public class TagParameters : QueryStringParameters
    {

    }
}
