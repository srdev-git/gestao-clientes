using SRDev.GestaoClientes.Domain.Exceptions;

namespace SRDev.GestaoClientes.Domain.ValueObjects
{
    public class Imagem
    {
        private static readonly HashSet<string> _formatosPermitidos = new()
        {
            "image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp"
        };

        public string Url { get; private set; }

        // Construtor protegido para evitar criação inválida
        protected Imagem() { }

        public Imagem(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new DomainException("URL da imagem não pode ser vazia.");

            Url = url;
        }

        public static void ValidarFormato(string contentType)
        {
            if (!_formatosPermitidos.Contains(contentType.ToLower()))
                throw new DomainException("Formato de imagem não permitido. Use JPEG, PNG, GIF, BMP ou WEBP.");
        }
    }
}
