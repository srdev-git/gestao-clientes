using MediatR;
using System;

namespace SRDev.GestaoClientes.Application.Clientes.Commands
{
    public class RemoverClienteCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public RemoverClienteCommand(Guid id)
        {
            Id = id;
        }
    }
}
