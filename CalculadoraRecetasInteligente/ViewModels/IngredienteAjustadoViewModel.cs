namespace CalculadoraRecetasInteligente.ViewModels
{
    public class IngredienteAjustadoViewModel
    {
        public string Nombre { get; set; } = string.Empty;

        public decimal CantidadOriginal { get; set; }

        public decimal NuevaCantidad { get; set; }

        public string UnidadMedida { get; set; } = string.Empty;
    }
}