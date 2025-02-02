using Bogus;
using SRDev.GestaoClientes.Domain.ClienteAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Tests.Fixtures
{
    public class LogradouroFixture
    {
        private readonly Faker _faker = new Faker("pt_BR");

        public Logradouro CriarLogradouroValido()
        {
            return new Logradouro(_faker.Address.StreetAddress());
        }

        public Logradouro CriarLogradouroInvalido()
        {
            return new Logradouro("");
        }
    }
}
