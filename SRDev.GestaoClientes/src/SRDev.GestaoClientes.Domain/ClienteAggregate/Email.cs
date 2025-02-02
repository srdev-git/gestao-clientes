using SRDev.GestaoClientes.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Domain.ClienteAggregate
{
    public class Email
    {
        public string Valor { get; private set; }

        protected Email() { }

        public Email(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor) || !valor.Contains("@"))
                throw new DomainException("E-mail inválido.");

            Valor = valor;
        }
    }
}
