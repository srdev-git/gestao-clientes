using MediatR;
using SRDev.GestaoClientes.Domain.ValueObjects;

namespace SRDev.GestaoClientes.Application.Queries.Cliente
{
    public class ObterClientePorIdQuery : IRequest<ClienteDto>
    {
        public Guid ClienteId { get; set; }
    }
}
