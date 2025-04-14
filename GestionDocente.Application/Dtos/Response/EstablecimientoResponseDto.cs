namespace GestionDocente.Application.Dtos.Response
{
    public class EstablecimientoResponseDto
    {
        public string Id { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string? Direccion { get; set; } = string.Empty;

        public string? Telefono { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public string? Website { get; set; } = string.Empty;
    }
}
