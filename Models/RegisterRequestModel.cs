using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Client.Models
{
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = "Kullanıcı adı alanı zorunludur!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur!")]
        [EmailAddress(ErrorMessage = "Geçersiz bir email adresi girdiniz!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola alanı zorunludur!")]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required(ErrorMessage = "Rol adı alanı zorunludur!")]
        public string Role { get; set; }
    }
}
