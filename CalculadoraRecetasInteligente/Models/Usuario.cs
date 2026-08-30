using System;
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
        [Column("rol_id")]
        public int RolId { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [Column("correo")]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [Column("password_hash")]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("activo")]
        public bool Activo { get; set; }

        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; }
    }
}