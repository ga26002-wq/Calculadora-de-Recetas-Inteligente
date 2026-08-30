using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculadoraRecetasInteligente.Models
{
    [Table("Usuarios", Schema = "seguridad")]
    public class Usuario
    {
        [Key]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("correo")]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [Column("contrasena")]
        public string Contrasena { get; set; } = string.Empty;

        [Column("rol_id")]
        public int RolId { get; set; }

        [Column("activo")]
        public bool Activo { get; set; }
    }
}