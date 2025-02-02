using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Application.Commands.Cliente
{
    public class CriarClienteCommand : IRequest<Guid>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        //public string Logotipo { get; set; }
        public List<string> Logradouros { get; set; } = new();
    }
}
