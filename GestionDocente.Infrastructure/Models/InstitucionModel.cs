namespace GestionDocente.Infrastructure.Models
{
    public class InstitucionModel : BaseModel
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Direccion { get; set; } = string.Empty;

        public string? Telefono { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public string? Website { get; set; } = string.Empty;
    }
}
