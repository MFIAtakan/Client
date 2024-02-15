    using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class LoginRequestModel
    {

        [Required(ErrorMessage = "Kullanıcı adı alanı zorunludur!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parola alanı zorunludur!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }    
    }
}
