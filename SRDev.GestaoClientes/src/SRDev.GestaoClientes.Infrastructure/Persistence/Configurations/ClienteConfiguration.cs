using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRDev.GestaoClientes.Domain.ClienteAggregate;

namespace SRDev.GestaoClientes.Infrastructure.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Logotipo)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.HasMany(c => c.Logradouros)
                .WithOne()
                .HasForeignKey(l => l.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.Email)
                .IsUnique();
        }
    }
}
