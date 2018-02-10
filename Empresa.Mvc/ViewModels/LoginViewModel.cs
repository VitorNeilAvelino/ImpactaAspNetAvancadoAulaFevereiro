using System.ComponentModel.DataAnnotations;

namespace Empresa.Mvc.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}