using MediatR;
using SRDev.GestaoClientes.Application.Queries.Cliente;
using SRDev.GestaoClientes.Domain.ClienteAggregate;
using SRDev.GestaoClientes.Domain.ValueObjects;

namespace SRDev.GestaoClientes.Application.Handlers.Cliente
{
    //TODO: Incluir logradouro
    public class ObterClientePorIdHandler : IRequestHandler<ObterClientePorIdQuery, ClienteDto>
    {
        private readonly IClienteRepository _repository;

        public ObterClientePorIdHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClienteDto> Handle(ObterClientePorIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _repository.ObterPorIdAsync(request.ClienteId);
            //TODO: Implementar automapper
            return cliente == null
                ? null
                : new ClienteDto
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Email = cliente.Email,
                    Logotipo = cliente.Logotipo,
                    Logradouros = cliente.Logradouros.Select(l => new LogradouroDto { Id = l.Id, Endereco = l.Endereco }).ToList()
                };

        }
    }
}
