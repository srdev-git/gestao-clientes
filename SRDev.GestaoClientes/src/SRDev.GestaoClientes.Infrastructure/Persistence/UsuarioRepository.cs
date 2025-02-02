using Microsoft.EntityFrameworkCore;
using SRDev.GestaoClientes.Domain.UsuarioAggregate;
using SRDev.GestaoClientes.Infrastructure.Persistence;
using System;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly GestaoClientesDbContext _context;

        public UsuarioRepository(GestaoClientesDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ObterPorIdAsync(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteEmailAsync(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }
    }
}
