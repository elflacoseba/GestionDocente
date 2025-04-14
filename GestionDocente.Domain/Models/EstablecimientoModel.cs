namespace GestionDocente.Domain.Models
{
    public class EstablecimientoModel : BaseModel
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Direccion { get; set; } = string.Empty;

        public string? Telefono { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public string? Website { get; set; } = string.Empty;
    }
}
