using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using SRDev.GestaoClientes.Application.Abstractions;

namespace SRDev.GestaoClientes.Infrastructure.Storage
{
    public class AzureBlobStorageService : IStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public AzureBlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureBlobStorage:ConnectionString"];
            var containerName = configuration["AzureBlobStorage:ContainerName"];
            _containerClient = new BlobContainerClient(connectionString, containerName);
        }

        public async Task<string> UploadArquivoAsync(string nomeArquivo, byte[] conteudo, string contentType)
        {
            var blobClient = _containerClient.GetBlobClient(nomeArquivo);
            using var stream = new MemoryStream(conteudo);

            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType });

            return blobClient.Uri.ToString();
        }
    }
}
