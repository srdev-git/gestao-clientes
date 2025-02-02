using MediatR;
using SRDev.GestaoClientes.Domain.ValueObjects;

namespace SRDev.GestaoClientes.Application.Queries.Cliente
{
    public class ObterClientesPaginadosQuery : IRequest<PaginacaoResultado<ClientePaginadoDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
