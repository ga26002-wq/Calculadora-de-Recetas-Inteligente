using System.Collections.Generic;

namespace CalculadoraRecetasInteligente.ViewModels
{
    public class ResultadoAjusteViewModel
    {
        public string NombreReceta { get; set; } = string.Empty;

        public int PorcionesOriginales { get; set; }

        public int NuevasPorciones { get; set; }

        public List<IngredienteAjustadoViewModel> Ingredientes { get; set; }
            = new List<IngredienteAjustadoViewModel>();
    }
}