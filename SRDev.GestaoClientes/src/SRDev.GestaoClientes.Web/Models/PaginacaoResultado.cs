namespace SRDev.GestaoClientes.Web.Models
{
    public class PaginacaoResultado<T>
    {
        public List<T> Itens { get; set; } = new();
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
    }
}
