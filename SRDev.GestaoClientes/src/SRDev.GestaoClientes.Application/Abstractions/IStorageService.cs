using System.Net.Mime;

namespace SRDev.GestaoClientes.Application.Abstractions
{
    public interface IStorageService
    {
        Task<string> UploadArquivoAsync(string nomeArquivo, byte[] conteudo, string contentType);
    }
}
