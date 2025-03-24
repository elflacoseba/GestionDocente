namespace GestionDocente.Application.Dtos.Request
{
    public class UpdateApplicationUserRequestDto
    {
        private string? _Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

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
