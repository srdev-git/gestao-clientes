using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRDev.GestaoClientes.Domain.UsuarioAggregate;

namespace SRDev.GestaoClientes.Infrastructure.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Define a tabela
            builder.ToTable("Usuarios");

            // Define a chave primária
            builder.HasKey(u => u.Id);

            // Configura o campo Nome
            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Configura o campo Email (deve ser único)
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            // Configura o campo SenhaHash
            builder.Property(u => u.SenhaHash)
                .IsRequired()
                .HasMaxLength(255);

            // Ignora propriedades caso existam métodos que o EF não precisa mapear
            // builder.Ignore(u => u.AlgumaPropriedadeNaoPersistida);
        }
    }
}
