using System.ComponentModel.DataAnnotations;

namespace MTAdmin.Shared.ViewModels.Client
{
    public class ContactVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Message { get; set; }
        public string IPAddress { get; set; }
    }
}
