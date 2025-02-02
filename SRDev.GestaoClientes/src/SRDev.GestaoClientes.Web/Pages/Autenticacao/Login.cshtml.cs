using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SRDev.GestaoClientes.Web.Services;

namespace SRDev.GestaoClientes.Web.Pages.Autenticacao
{
    public class LoginModel : PageModel
    {
        private readonly AutenticacaoService _autenticacaoService;

        public LoginModel(AutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Senha { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = await _autenticacaoService.LoginAsync(Email, Senha);

            if (string.IsNullOrEmpty(token))
            {
                ErrorMessage = "Credenciais inválidas.";
                return Page();
            }

            HttpContext.Session.SetString("AuthToken", token);
            return LocalRedirect("/Clientes/Index");
        }
    }
}
