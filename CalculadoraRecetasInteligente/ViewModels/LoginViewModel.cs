using System.ComponentModel.DataAnnotations;

namespace CalculadoraRecetasInteligente.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingrese el correo")]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese la contraseña")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = string.Empty;
    }
}