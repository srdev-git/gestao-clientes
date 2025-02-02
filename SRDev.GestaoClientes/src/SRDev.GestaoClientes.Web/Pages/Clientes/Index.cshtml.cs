using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SRDev.GestaoClientes.Web.Services;
using SRDev.GestaoClientes.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Web.Pages.Clientes
{
    public class IndexModel : PageModelBase
    {
        private readonly ClienteService _clienteService;

        public IndexModel(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public List<ClienteViewModel> Clientes { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _clienteService.ObterClientesPaginados(PageNumber, PageSize);
            Clientes = result.Itens;
            return Page();
        }
    }
}
