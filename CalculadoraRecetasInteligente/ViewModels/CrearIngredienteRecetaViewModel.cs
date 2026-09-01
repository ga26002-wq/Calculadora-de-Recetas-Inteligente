using System.ComponentModel.DataAnnotations;

namespace CalculadoraRecetasInteligente.ViewModels
{
    public class CrearIngredienteRecetaViewModel
    {
        [Required]
        public int RecetaId { get; set; }

        [Required(ErrorMessage = "El nombre del ingrediente es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(0.01, 999999, ErrorMessage = "La cantidad debe ser mayor que cero")]
        public decimal Cantidad { get; set; }

        [Required(ErrorMessage = "La unidad de medida es obligatoria")]
        [StringLength(30)]
        public string UnidadMedida { get; set; } = string.Empty;
    }
}