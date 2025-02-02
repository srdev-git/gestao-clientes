using Bogus;
using SRDev.GestaoClientes.Domain.ClienteAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Tests.Fixtures
{
    public class ClienteFixture
    {
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly LogradouroFixture _logradouroFixture = new();

        public Cliente CriarClienteValido()
        {
            return new Cliente(
                _faker.Person.FullName,
                _faker.Internet.Email()/*,
                _faker.Image.PicsumUrl()*/
            );
        }

        public Cliente CriarClienteSemNome()
        {
            return new Cliente(
                "",
                _faker.Internet.Email()/*,
                _faker.Image.PicsumUrl()*/
            );
        }

        public Cliente CriarClienteComEmailInvalido()
        {
            return new Cliente(
                _faker.Person.FullName,
                "email_invalido.com"/*,
                _faker.Image.PicsumUrl()*/
            );
        }

        public Logradouro CriarLogradouroValido()
        {
            return _logradouroFixture.CriarLogradouroValido();
        }

        public Logradouro CriarLogradouroInvalido()
        {
            return _logradouroFixture.CriarLogradouroInvalido();
        }
    }
}
