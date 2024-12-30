using MTAdmin.Shared.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MTAdmin.Shared.ViewModels
{
    public class CreateLanguageVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string NativeName { get; set; }
        public string Slug { get; set; }
        public bool IsActive { get; set; }
    }

    public class LanguageVM : CreateLanguageVM
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

    public class LanguagePageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
        public bool IsActive { get; set; }
        public int TemplateCount { get; set; }
        public float TemplateUsage { get; set; }        
    }

    public class LanguageParameters : QueryStringParameters
    {

    }
}
