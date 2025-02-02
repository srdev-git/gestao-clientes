using SRDev.GestaoClientes.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Domain.ClienteAggregate
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string? Logotipo { get; private set; } // Caminho da imagem no banco ou storage
        private List<Logradouro> _logradouros = new();
        public IReadOnlyCollection<Logradouro> Logradouros => _logradouros.AsReadOnly();

        // Construtor protegido para evitar criação inválida
        protected Cliente() { }

        public Cliente(string nome, string email)
        {
            AtualizaDadosCliente(nome, email);

            Id = Guid.NewGuid();
        }

        private void AtualizaDadosCliente(string nome, string email)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new DomainException("Nome não pode ser vazio.");
            if (!email.Contains("@")) throw new DomainException("E-mail inválido.");
            Nome = nome;
            Email = email;
        }        

        public void Atualizar(string nome, string email)
        {
            AtualizaDadosCliente(nome, email);
        }

        public void AdicionarLogradouro(Logradouro logradouro)
        {
            if (logradouro == null) throw new DomainException("Logradouro inválido.");
            _logradouros.Add(logradouro);
        }

        public void AtualizarLogradouro(Guid id, string novoEndereco)
        {
            var logradouro = _logradouros.FirstOrDefault(l => l.Id == id);
            if (logradouro == null) throw new DomainException("Logradouro não encontrado.");
            logradouro.Atualizar(novoEndereco);
        }

        public void RemoverLogradouro(Logradouro logradouro)
        {
            if (!_logradouros.Contains(logradouro)) throw new DomainException("Logradouro não encontrado.");
            _logradouros.Remove(logradouro);
        }

        public void AtualizarLogotipo(string? logotipoUrl)
        {
            Logotipo = logotipoUrl;
        }
    }
}
