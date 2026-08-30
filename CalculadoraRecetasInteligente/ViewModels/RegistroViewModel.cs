using System.ComponentModel.DataAnnotations;

namespace CalculadoraRecetasInteligente.ViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Ingrese su nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese su correo")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese una contraseña")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Contrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirme su contraseña")]
        [DataType(DataType.Password)]
        [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContrasena { get; set; } = string.Empty;
    }
}