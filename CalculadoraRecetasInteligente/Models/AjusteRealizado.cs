using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculadoraRecetasInteligente.Models
{
    [Table("AjustesRealizados", Schema = "recetas")]
    public class AjusteRealizado
    {
        [Key]
        [Column("ajuste_id")]
        public int AjusteId { get; set; }

        [Required]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Required]
        [Column("receta_id")]
        public int RecetaId { get; set; }

        [Required]
        [Column("porciones_originales")]
        public int PorcionesOriginales { get; set; }

        [Required]
        [Column("nuevas_porciones")]
        public int NuevasPorciones { get; set; }

        [Column("fecha_ajuste")]
        public DateTime FechaAjuste { get; set; }
    }
}