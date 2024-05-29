using System.ComponentModel.DataAnnotations;

namespace LoginApplication.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        public string? Fullname { get; set; }

        [Required]
        public string Password { get; set; }
    }
}