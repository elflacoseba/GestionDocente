namespace GestionDocente.Application.Dtos.Request
{
    public class UpdateApplicationRoleRequestDto
    {
        private string? _Id;
        public string? Name { get; set; }
        public string? Description { get; set; }

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
