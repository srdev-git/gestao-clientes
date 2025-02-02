using MediatR;
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
    public class CriarClienteHandler : IRequestHandler<CriarClienteCommand, Guid>
    {
        private readonly IClienteRepository _clienteRepository;

        public CriarClienteHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Guid> Handle(CriarClienteCommand request, CancellationToken cancellationToken)
        {
            bool emailExiste = await _clienteRepository.EmailExisteAsync(request.Email);
            if (emailExiste)
                throw new EmailJaCadastradoException(request.Email);


            var cliente = new Domain.ClienteAggregate.Cliente(request.Nome, request.Email/*, request.Logotipo*/);

            request.Logradouros?.ForEach(logradouro =>
            {
                cliente.AdicionarLogradouro(new Logradouro(logradouro));
            });

            await _clienteRepository.AdicionarAsync(cliente);
            return cliente.Id;
        }
    }
}
