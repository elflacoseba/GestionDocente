using GestionDocente.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionDocente.Infrastructure.Persistences.Context.Configurations
{
    public class EstablecimientoConfiguration : IEntityTypeConfiguration<EstablecimientoModel>
    {
        public void Configure(EntityTypeBuilder<EstablecimientoModel> builder)
        {
            builder.ToTable("Establecimientos");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsRequired()
                .IsUnicode(false);
            builder.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Website)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.FechaCreacion)
                .HasColumnType("datetime");
            builder.Property(e => e.FechaActualizacion)
                .HasColumnType("datetime");
        }
    }
}