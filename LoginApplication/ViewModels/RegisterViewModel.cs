using System.ComponentModel.DataAnnotations;

namespace LoginApplication.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Fullname { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
