namespace GestionDocente.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public int UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public Guid? UsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public bool Activo { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
