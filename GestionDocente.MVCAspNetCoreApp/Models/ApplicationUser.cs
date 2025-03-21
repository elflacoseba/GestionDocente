namespace GestionDocente.MVCAspNetCoreApp.Models
{
    public class ApplicationUser
    {
        public string? Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        
        public string FullName => $"{FirstName} {LastName}".Trim();

        public bool IsActive { get; set; }


        public ApplicationUser()
        {
            
        }
    }
}
