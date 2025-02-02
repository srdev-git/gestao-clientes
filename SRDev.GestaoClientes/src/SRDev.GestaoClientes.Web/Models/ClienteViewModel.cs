using System;
using System.Collections.Generic;

namespace SRDev.GestaoClientes.Web.Models
{
    using System;
    using System.Collections.Generic;

    public class ClientePaginadoViewModel
    {
        public ClientePaginadoViewModel() => Itens = new List<ClienteViewModel>();
        public List<ClienteViewModel> Itens { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalItens { get; set; }
    }

    public class ClienteViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Logotipo { get; set; }
    }
}
