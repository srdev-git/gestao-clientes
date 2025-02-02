using SRDev.GestaoClientes.Domain.Exceptions;
using SRDev.GestaoClientes.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Tests.ClienteAggregate
{
    public class EmailTests : IClassFixture<EmailFixture>
    {
        private readonly EmailFixture _fixture;

        public EmailTests(EmailFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Deve_Criar_Email_Com_Valor_Valido()
        {
            // Arrange & Act
            var email = _fixture.CriarEmailValido();

            // Assert
            Assert.NotNull(email);
            Assert.Contains("@", email.Valor);
        }

        [Fact]
        public void Deve_Lancar_DomainException_Quando_Email_Nao_Tiver_Arroba()
        {
            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => _fixture.CriarEmailInvalidoSemArroba());

            Assert.Equal("E-mail inválido.", exception.Message);
        }

        [Fact]
        public void Deve_Lancar_DomainException_Quando_Email_For_Vazio()
        {
            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => _fixture.CriarEmailVazio());

            Assert.Equal("E-mail inválido.", exception.Message);
        }
    }
}
