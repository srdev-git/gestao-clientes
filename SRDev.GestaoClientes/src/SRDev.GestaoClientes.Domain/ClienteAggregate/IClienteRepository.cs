using SRDev.GestaoClientes.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Domain.ClienteAggregate
{
    public interface IClienteRepository
    {
        Task AdicionarAsync(Cliente cliente);
        Task<Cliente> ObterPorIdAsync(Guid id);
        Task<bool> AtualizarAsync(Cliente cliente);
        Task RemoverAsync(Guid id);
        Task<bool> EmailExisteAsync(string email);
        Task<PaginacaoResultado<ClientePaginadoDto>> ObterClientesPaginadosAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
