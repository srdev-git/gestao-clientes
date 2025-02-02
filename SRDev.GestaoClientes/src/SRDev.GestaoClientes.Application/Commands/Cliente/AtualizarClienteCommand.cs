using MediatR;
using SRDev.GestaoClientes.Domain.ValueObjects;

namespace SRDev.GestaoClientes.Application.Commands.Cliente
{
    public class AtualizarClienteCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        // public string Logotipo { get; set; }
        public List<LogradouroDto> Logradouros { get; set; } = new();
    }
}
