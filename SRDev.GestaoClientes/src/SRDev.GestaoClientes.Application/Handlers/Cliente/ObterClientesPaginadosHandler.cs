using MediatR;
using SRDev.GestaoClientes.Application.Queries.Cliente;
using SRDev.GestaoClientes.Domain.ClienteAggregate;
using SRDev.GestaoClientes.Domain.ValueObjects;

namespace SRDev.GestaoClientes.Application.Handlers.Cliente
{
    public class ObterClientesPaginadosHandler : IRequestHandler<ObterClientesPaginadosQuery, PaginacaoResultado<ClientePaginadoDto>>
    {
        private readonly IClienteRepository _clienteRepository;

        public ObterClientesPaginadosHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<PaginacaoResultado<ClientePaginadoDto>> Handle(ObterClientesPaginadosQuery request, CancellationToken cancellationToken)
        {
            return await _clienteRepository.ObterClientesPaginadosAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
