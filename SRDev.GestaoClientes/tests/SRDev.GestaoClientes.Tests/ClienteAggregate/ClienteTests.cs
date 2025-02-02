using Bogus;
using SRDev.GestaoClientes.Domain.ClienteAggregate;
using SRDev.GestaoClientes.Domain.Exceptions;
using SRDev.GestaoClientes.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Tests.ClienteAggregate
{
    public class ClienteTests : IClassFixture<ClienteFixture>
    {
        private readonly ClienteFixture _fixture;

        public ClienteTests(ClienteFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Deve_Criar_Cliente_Com_Dados_Validos()
        {
            // Arrange & Act
            var cliente = _fixture.CriarClienteValido();

            // Assert
            Assert.NotNull(cliente);
            Assert.NotEmpty(cliente.Nome);
            Assert.Contains("@", cliente.Email);
            Assert.NotNull(cliente.Logotipo);
        }

        [Fact]
        public void Deve_Lancar_DomainException_Quando_Nome_For_Vazio()
        {
            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => _fixture.CriarClienteSemNome());

            Assert.NotNull(exception.Message);
        }

        [Fact]
        public void Deve_Lancar_DomainException_Quando_Email_For_Invalido()
        {
            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => _fixture.CriarClienteComEmailInvalido());

            Assert.NotNull(exception.Message);
        }

        [Fact]
        public void Deve_Adicionar_Logradouro_Valido()
        {
            // Arrange
            var cliente = _fixture.CriarClienteValido();
            var logradouro = _fixture.CriarLogradouroValido();

            // Act
            cliente.AdicionarLogradouro(logradouro);

            // Assert
            Assert.Contains(logradouro, cliente.Logradouros);
        }

        [Fact]
        public void Deve_Lancar_DomainException_Ao_Adicionar_Logradouro_Invalido()
        {
            // Arrange
            var cliente = _fixture.CriarClienteValido();
            Logradouro logradouroInvalido = null;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => cliente.AdicionarLogradouro(logradouroInvalido));

            Assert.NotNull(exception.Message);
        }
    }
}
