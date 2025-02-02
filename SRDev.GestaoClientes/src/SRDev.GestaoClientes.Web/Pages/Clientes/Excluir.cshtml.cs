using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SRDev.GestaoClientes.Web.Models;
using SRDev.GestaoClientes.Web.Services;

namespace SRDev.GestaoClientes.Web.Pages.Clientes
{
    public class ExcluirModel : PageModelBase
    {
        private readonly ClienteService _clienteService;

        public ExcluirModel(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [BindProperty]
        public ClienteDetalheViewModel Cliente { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var result = await _clienteService.ObterClienteDetalhe(id);
            Cliente = result;
            return Page();
        }        

        public async Task<IActionResult> OnPostAsync()
        {
            if (Cliente?.Id == null || Cliente.Id == Guid.Empty)
            {
                return BadRequest("ID inválido.");
            }

            try
            {
                await _clienteService.ExcluirCliente(Cliente.Id);

                TempData["MensagemSucesso"] = "Cliente excluído com sucesso!";
                return RedirectToPage("/Clientes/Index");
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao excluir o cliente.";
                return RedirectToPage("/Clientes/Index");
            }
        }
    }
}
