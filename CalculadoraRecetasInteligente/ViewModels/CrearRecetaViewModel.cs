using System.ComponentModel.DataAnnotations;

namespace CalculadoraRecetasInteligente.ViewModels
{
    public class CrearRecetaViewModel
    {
        [Required(ErrorMessage = "Ingrese el nombre de la receta")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Ingrese el número de porciones")]
        [Range(1, 100, ErrorMessage = "Las porciones deben estar entre 1 y 100")]
        public int Porciones { get; set; }

        [Required(ErrorMessage = "Ingrese el tiempo de preparación")]
        [Range(0, 1440, ErrorMessage = "Ingrese un tiempo válido")]
        public int TiempoPreparacionMin { get; set; }

        [Required(ErrorMessage = "Ingrese el tiempo de cocción")]
        [Range(0, 1440, ErrorMessage = "Ingrese un tiempo válido")]
        public int TiempoCoccionMin { get; set; }
    }
}