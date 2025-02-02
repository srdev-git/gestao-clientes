using MediatR;

namespace SRDev.GestaoClientes.Application.Autenticacao
{
    public class LoginCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
