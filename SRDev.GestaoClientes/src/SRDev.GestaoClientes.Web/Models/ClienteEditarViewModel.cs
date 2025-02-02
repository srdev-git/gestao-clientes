namespace SRDev.GestaoClientes.Web.Models
{
    public class ClienteEditarViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Logradouro> Logradouros { get; set; }
    }
}
