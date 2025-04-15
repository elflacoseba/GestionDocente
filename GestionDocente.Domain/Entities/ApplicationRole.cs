namespace GestionDocente.Domain.Entities
{
    public class ApplicationRole
    {
        /// <summary>
        /// Obtiene o establece la clave principal para este rol.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre para este rol.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre normalizado para este rol.
        /// </summary>
        public string? NormalizedName { get; set; }

        /// <summary>
        /// Un valor aleatorio que debe cambiar cada vez que un rol se persiste en el almacenamiento.
        /// </summary>
        public string? ConcurrencyStamp { get; set; }

        /// <summary>
        /// Obtiene o establece una descripción para este rol.
        /// </summary>
        public string? Description { get; set; }

        public ApplicationRole()
        {
            Id = Guid.NewGuid().ToString();
            ConcurrencyStamp =  Guid.NewGuid().ToString();
        }
    }
}
