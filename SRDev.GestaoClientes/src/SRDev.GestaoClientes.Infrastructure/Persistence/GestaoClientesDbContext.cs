using SRDev.GestaoClientes.Domain.ClienteAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using SRDev.GestaoClientes.Domain.UsuarioAggregate;

namespace SRDev.GestaoClientes.Infrastructure.Persistence
{
    public class GestaoClientesDbContext : DbContext
    {
        public GestaoClientesDbContext(DbContextOptions<GestaoClientesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicando configurações do Fluent API
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoClientesDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
