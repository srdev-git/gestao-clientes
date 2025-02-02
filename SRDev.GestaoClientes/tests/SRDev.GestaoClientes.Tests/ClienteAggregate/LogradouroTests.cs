using SRDev.GestaoClientes.Domain.Exceptions;
using SRDev.GestaoClientes.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SRDev.GestaoClientes.Tests.ClienteAggregate
{
    public class LogradouroTests : IClassFixture<LogradouroFixture>
    {
        private readonly LogradouroFixture _fixture;

        public LogradouroTests(LogradouroFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Deve_Criar_Logradouro_Com_Endereco_Valido()
        {
            var logradouro = _fixture.CriarLogradouroValido();
            Assert.NotNull(logradouro);
            Assert.NotEmpty(logradouro.Endereco);
        }

        [Fact]
        public void Nao_Deve_Criar_Logradouro_Com_Endereco_Invalido()
        {
            var exception = Assert.Throws<DomainException>(() => _fixture.CriarLogradouroInvalido());
            Assert.NotNull(exception.Message);
        }
    }
}
