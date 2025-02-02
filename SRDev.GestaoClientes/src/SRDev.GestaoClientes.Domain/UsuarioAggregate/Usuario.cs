using SRDev.GestaoClientes.Domain.Exceptions;
using System;

namespace SRDev.GestaoClientes.Domain.UsuarioAggregate
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }

        protected Usuario() { } 

        public Usuario(string nome, string email, string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new DomainException("Nome não pode ser vazio.");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) throw new DomainException("E-mail inválido.");
            if (string.IsNullOrWhiteSpace(senhaHash)) throw new DomainException("Senha inválida.");

            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
        }

        public void AtualizarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new DomainException("Nome não pode ser vazio.");
            Nome = nome;
        }

        public void AtualizarSenha(string novaSenhaHash)
        {
            if (string.IsNullOrWhiteSpace(novaSenhaHash)) throw new DomainException("Senha inválida.");
            SenhaHash = novaSenhaHash;
        }
    }
}
