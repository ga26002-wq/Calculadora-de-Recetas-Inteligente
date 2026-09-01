using System.ComponentModel.DataAnnotations;

namespace CalculadoraRecetasInteligente.ViewModels
{
    public class AjusteInteligenteViewModel
    {
        [Required(ErrorMessage = "Selecciona una receta")]
        public int RecetaId { get; set; }

        public string NombreReceta { get; set; } = string.Empty;

        public int PorcionesActuales { get; set; }

        [Required(ErrorMessage = "Ingresa la nueva cantidad de porciones")]
        [Range(1, 100, ErrorMessage = "Las porciones deben estar entre 1 y 100")]
        public int NuevasPorciones { get; set; }
    }
}