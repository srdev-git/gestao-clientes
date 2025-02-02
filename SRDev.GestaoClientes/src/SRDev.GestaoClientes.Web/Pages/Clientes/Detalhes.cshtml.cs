using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SRDev.GestaoClientes.Web.Models;
using SRDev.GestaoClientes.Web.Services;
using System.Net.Http;
using System.Text.Json;

namespace SRDev.GestaoClientes.Web.Pages.Clientes
{
    public class DetalhesModel : PageModelBase
    {

        private readonly ClienteService _clienteService;

        public DetalhesModel(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [BindProperty]
        public ClienteDetalheViewModel Cliente { get; set; }

        public string MensagemSucesso { get; set; }
        public string MensagemErro { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var result = await _clienteService.ObterClienteDetalhe(id);
            Cliente = result;
            return Page();
        }

        public async Task<IActionResult> OnPostUploadLogotipoAsync(Guid id, IFormFile logotipoFile)
        {
            if (logotipoFile == null || logotipoFile.Length == 0)
            {
                MensagemErro = "Selecione um arquivo válido.";
                return await OnGetAsync(id);
            }

            try
            {
                var result = await _clienteService.UploadLogotipo(id, logotipoFile);

                Cliente.Logotipo = result?.UrlImagem ?? Cliente.Logotipo;
                MensagemSucesso = "Logotipo enviada com sucesso!";
            }
            catch (Exception)
            {
                MensagemErro = "Erro ao enviar a logotipo.";
            }

            return await OnGetAsync(id);
        }

        public class UploadResponse
        {
            public string Mensagem { get; set; }
            public string UrlImagem { get; set; }
        }
    }
}
