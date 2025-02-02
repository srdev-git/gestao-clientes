using MediatR;
using SRDev.GestaoClientes.Application.Clientes.Commands;
using SRDev.GestaoClientes.Application.Commands.Cliente;
using SRDev.GestaoClientes.Domain.ClienteAggregate;
using SRDev.GestaoClientes.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Application.Handlers.Cliente
{
    //TODO: Implementar exclusão lógica
    public class RemoverClienteHandler : IRequestHandler<RemoverClienteCommand, bool>
    {
        private readonly IClienteRepository _clienteRepository;

        public RemoverClienteHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(RemoverClienteCommand request, CancellationToken cancellationToken)
        {
            await _clienteRepository.RemoverAsync(request.Id);
            return true;
        }
    }
}
