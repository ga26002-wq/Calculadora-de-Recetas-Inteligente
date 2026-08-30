using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculadoraRecetasInteligente.Models
{
    [Table("Roles", Schema = "seguridad")]
    public class Rol
    {
        [Key]
        [Column("rol_id")]
        public int RolId { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;
    }
}