using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SRDev.GestaoClientes.Web.Models;
using SRDev.GestaoClientes.Web.Services;

namespace SRDev.GestaoClientes.Web.Pages.Clientes
{
    public class EditarModel : PageModelBase
    {
        private readonly ClienteService _clienteService;

        public EditarModel(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [BindProperty(SupportsGet = true)]
        public ClienteEditarViewModel Cliente { get; set; }

        public string MensagemErro { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var clienteDetalhe = await _clienteService.ObterClienteDetalhe(id);
            Cliente = new ClienteEditarViewModel
            {
                Id = clienteDetalhe.Id,
                Email = clienteDetalhe.Email,
                Logradouros = clienteDetalhe.Logradouros,
                Nome = clienteDetalhe.Nome
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Cliente?.Id == null || Cliente.Id == Guid.Empty)
            {
                return BadRequest("ID inválido.");
            }

            try
            {
                await _clienteService.AtualizarCliente(Cliente);

                TempData["MensagemSucesso"] = "Cliente atualizado com sucesso!";
                return RedirectToPage("/Clientes/Index");
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao atualizar o cliente.";
                return RedirectToPage("/Clientes/Index");
            }
        }

        public PartialViewResult OnPostAdicionarEndereco()
        {
            if (Cliente.Logradouros == null)
            {
                Cliente.Logradouros = new List<Logradouro>();
            }

            Cliente.Logradouros.Add(new Logradouro { Id = Guid.NewGuid(), Endereco = "" });

            return Partial("_TabelaLogradouros", Cliente.Logradouros);
        }

        //public IActionResult OnPostAdicionarEndereco()
        //{
        //    if (Cliente.Logradouros == null)
        //    {
        //        Cliente.Logradouros = new List<Logradouro>();
        //    }

        //    Cliente.Logradouros.Add(new Logradouro { Id = Guid.Empty, Endereco = "" });

        //    return Page();
        //}

        //public IActionResult OnPostRemoverEndereco(int index)
        //{
        //    if (Cliente.Logradouros != null && index >= 0 && index < Cliente.Logradouros.Count)
        //    {
        //        Cliente.Logradouros.RemoveAt(index);
        //    }

        //    return Page();
        //}

        public IActionResult OnPostRemoverEndereco(int index)
        {
            if (Cliente.Logradouros != null && index >= 0 && index < Cliente.Logradouros.Count)
            {
                Cliente.Logradouros.RemoveAt(index);
            }

            return Partial("_TabelaLogradouros", Cliente); // Retorna ClienteEditarViewModel corretamente
        }
    }
}
