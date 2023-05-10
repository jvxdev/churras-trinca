using System.ComponentModel.DataAnnotations;

namespace ChurrasTrinca.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType (DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Lembrar")]
        public bool RememberMe { get; set; }
    }
}
