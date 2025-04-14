namespace GestionDocente.Domain.Models
{
    public abstract class BaseModel
    {
        public string Id { get; set; } = string.Empty;

        public string UsuarioCreacion { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }

        public string? UsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public bool Activo { get; set; }
    }
}
