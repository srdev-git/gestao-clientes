namespace SRDev.GestaoClientes.Web.Models
{
    public class UploadImagemViewModel
    {
        public Guid ClienteId { get; set; }
        public IFormFile Imagem { get; set; }
    }
}
