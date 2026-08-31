using System.ComponentModel.DataAnnotations;

namespace CalculadoraRecetasInteligente.ViewModels
{
    public class EditarRecetaViewModel
    {
        public int RecetaId { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre de la receta")]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        [Range(1, 100, ErrorMessage = "Las porciones deben ser mayores a 0")]
        public int Porciones { get; set; }

        [Range(0, 1440, ErrorMessage = "Ingrese un tiempo válido")]
        public int TiempoPreparacionMin { get; set; }

        [Range(0, 1440, ErrorMessage = "Ingrese un tiempo válido")]
        public int TiempoCoccionMin { get; set; }
    }
}