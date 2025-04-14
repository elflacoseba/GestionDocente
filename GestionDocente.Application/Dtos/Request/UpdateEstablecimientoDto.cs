namespace GestionDocente.Application.Dtos.Request
{
    public class UpdateEstablecimientoDto
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }

        private string? _Id { get; set; } = string.Empty;

        public void SetId(string? id)
        {
            _Id = id;
        }

        public string? GetId()
        {
            return _Id;
        }
    }
}
