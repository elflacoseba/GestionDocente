namespace GestionDocente.Application.Dtos.Request
{
    public class CreateApplicationRoleRequestDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

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
