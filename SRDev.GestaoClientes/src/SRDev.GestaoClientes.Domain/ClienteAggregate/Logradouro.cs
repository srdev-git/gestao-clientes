using SRDev.GestaoClientes.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Domain.ClienteAggregate
{
    public class Logradouro
    {
        public Guid Id { get; private set; }
        public string Endereco { get; private set; }
        public Guid ClienteId { get; /*private*/ set; } // Relacionamento com Cliente

        protected Logradouro() { }

        public Logradouro(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new DomainException("Endereço não pode ser vazio.");

            Id = Guid.NewGuid();
            Endereco = endereco;
        }

        public void Atualizar(string novoEndereco)
        {
            if (string.IsNullOrWhiteSpace(novoEndereco))
                throw new DomainException("Endereço não pode ser vazio.");

            Endereco = novoEndereco;
        }
    }
}
