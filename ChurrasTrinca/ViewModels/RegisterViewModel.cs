using System.ComponentModel.DataAnnotations;

namespace ChurrasTrinca.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo E-mail é obrigatório!")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "O campo Confirmar senha é obrigatório!")]
        [Display(Name = "Confirmar senha")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "As senhas não conferem!")]
        public string? ConfirmPassword { get; set; } 
    }
}
