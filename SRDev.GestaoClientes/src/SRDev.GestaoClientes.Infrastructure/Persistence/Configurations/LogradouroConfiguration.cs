using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRDev.GestaoClientes.Domain.ClienteAggregate;

namespace SRDev.GestaoClientes.Infrastructure.Persistence.Configurations
{
    public class LogradouroConfiguration : IEntityTypeConfiguration<Logradouro>
    {
        public void Configure(EntityTypeBuilder<Logradouro> builder)
        {
            builder.ToTable("Logradouros");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Endereco)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne<Cliente>()
                .WithMany(c => c.Logradouros)
                .HasForeignKey(l => l.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

