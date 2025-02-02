namespace SRDev.GestaoClientes.Domain.ValueObjects
{
    public class PaginacaoResultado<T>
    {
        public List<T> Itens { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalItens { get; set; }
    }
}
