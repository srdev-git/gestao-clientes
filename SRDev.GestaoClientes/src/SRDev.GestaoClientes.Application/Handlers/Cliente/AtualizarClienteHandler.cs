using MediatR;
using SRDev.GestaoClientes.Application.Commands.Cliente;
using SRDev.GestaoClientes.Domain.ClienteAggregate;
using SRDev.GestaoClientes.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Application.Handlers.Cliente
{
    //TODO: Incluir logradouro
    public class AtualizarClienteHandler : IRequestHandler<AtualizarClienteCommand, bool>
    {
        private readonly IClienteRepository _repository;

        public AtualizarClienteHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            // Buscar cliente existente no repositório
            var clienteExistente = await _repository.ObterPorIdAsync(request.Id);
            if (clienteExistente == null)
                throw new DomainException("Cliente não encontrado.");

            // Atualizar dados do cliente
            clienteExistente.Atualizar(request.Nome, request.Email/*, request.Logotipo*/);

            // Obter os IDs dos logradouros na requisição
            var idsEnderecosRequest = request.Logradouros.Select(l => l.Id).ToList();

            // Remover logradouros que não estão mais na lista
            var enderecosParaRemover = clienteExistente.Logradouros
                .Where(l => !idsEnderecosRequest.Contains(l.Id))
                .ToList();

            foreach (var logradouro in enderecosParaRemover)
            {
                clienteExistente.RemoverLogradouro(logradouro);
            }

            // Atualizar ou adicionar logradouros
            foreach (var novoLogradouro in request.Logradouros)
            {
                var logradouroExistente = clienteExistente.Logradouros
                    .FirstOrDefault(l => l.Id == novoLogradouro.Id);

                if (logradouroExistente != null)
                {
                    // Atualiza os dados do logradouro existente
                    logradouroExistente.Atualizar(novoLogradouro.Endereco);
                }
                else
                {
                    // Adiciona um novo logradouro
                    clienteExistente.AdicionarLogradouro(new Logradouro(novoLogradouro.Endereco));
                }
            }

            // Persistir mudanças no repositório
            return await _repository.AtualizarAsync(clienteExistente);
        }
    }
}
