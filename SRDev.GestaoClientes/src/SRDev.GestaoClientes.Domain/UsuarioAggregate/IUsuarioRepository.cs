using System;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Domain.UsuarioAggregate
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterPorIdAsync(Guid id);
        Task<Usuario> ObterPorEmailAsync(string email);
        Task AdicionarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task<bool> ExisteEmailAsync(string email);
    }
}
