using System.ComponentModel.DataAnnotations;

namespace CalculadoraRecetasInteligente.Models
{
    public class Ingrediente
    {
        public int IngredienteId { get; set; }

        [Required(ErrorMessage = "El nombre del ingrediente es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        public decimal Cantidad { get; set; }

        [StringLength(30)]
        public string UnidadMedida { get; set; } = string.Empty;

        public int? RecetaId { get; set; }

        public Receta? Receta { get; set; }
    }
}