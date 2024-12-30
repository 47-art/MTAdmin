using System.ComponentModel.DataAnnotations;

namespace MTAdmin.Shared.ViewModels
{
    public class LoginVm
    {
        [Required]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
