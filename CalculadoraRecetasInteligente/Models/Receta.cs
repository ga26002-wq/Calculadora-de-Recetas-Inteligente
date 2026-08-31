using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculadoraRecetasInteligente.Models
{
    [Table("Recetas", Schema = "recetas")]
    public class Receta
    {
        [Key]
        [Column("receta_id")]
        public int RecetaId { get; set; }

        [Required]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("porciones")]
        public int Porciones { get; set; }

        [Column("tiempo_preparacion_min")]
        public int TiempoPreparacionMin { get; set; }

        [Column("tiempo_coccion_min")]
        public int TiempoCoccionMin { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        [Column("fecha_actualizacion")]
        public DateTime? FechaActualizacion { get; set; }
    }
}