using MediatR;
using SRDev.GestaoClientes.Application.Abstractions;
using SRDev.GestaoClientes.Domain.ClienteAggregate;
using SRDev.GestaoClientes.Domain.Exceptions;
using SRDev.GestaoClientes.Domain.ValueObjects;

namespace SRDev.GestaoClientes.Application.Clientes.Commands.Handlers
{
    public class UploadImagemHandler : IRequestHandler<UploadImagemCommand, string>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IStorageService _storageService;

        public UploadImagemHandler(IClienteRepository clienteRepository, IStorageService storageService)
        {
            _clienteRepository = clienteRepository;
            _storageService = storageService;
        }

        public async Task<string> Handle(UploadImagemCommand request, CancellationToken cancellationToken)
        {
            Imagem.ValidarFormato(request.ContentType);

            var cliente = await _clienteRepository.ObterPorIdAsync(request.ClienteId);
            if (cliente == null)
                throw new DomainException("Cliente não encontrado.");

            // Faz o upload do arquivo para o serviço de armazenamento externo
            var urlImagem = await _storageService.UploadArquivoAsync(request.NomeArquivo, request.Conteudo, request.ContentType);

            // Atualiza a URL da imagem no cliente
            cliente.AtualizarLogotipo(urlImagem);
            await _clienteRepository.AtualizarAsync(cliente);

            return urlImagem;
        }
    }
}
