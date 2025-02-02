using MediatR;

namespace SRDev.GestaoClientes.Application.Clientes.Commands
{
    public class UploadImagemCommand : IRequest<string>
    {
        public Guid ClienteId { get; }
        public string NomeArquivo { get; }
        public string ContentType { get; set; }
        public byte[] Conteudo { get; }

        public UploadImagemCommand(Guid clienteId, string nomeArquivo, byte[] conteudo, string contentType)
        {
            ClienteId = clienteId;
            Conteudo = conteudo ?? throw new ArgumentNullException(nameof(conteudo));
            NomeArquivo = string.IsNullOrWhiteSpace(nomeArquivo) ? throw new ArgumentNullException(nameof(nomeArquivo)) : nomeArquivo;
            ContentType = string.IsNullOrWhiteSpace(contentType) ? throw new ArgumentNullException(nameof(contentType)) : contentType;
        }
    }
}
