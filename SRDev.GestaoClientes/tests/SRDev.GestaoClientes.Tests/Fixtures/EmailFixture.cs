using Bogus;
using SRDev.GestaoClientes.Domain.ClienteAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Tests.Fixtures
{
    public class EmailFixture
    {
        private readonly Faker _faker = new Faker("pt_BR");

        public Email CriarEmailValido()
        {
            return new Email(_faker.Internet.Email());
        }

        public Email CriarEmailInvalidoSemArroba()
        {
            return new Email("email_invalido.com");
        }

        public Email CriarEmailVazio()
        {
            return new Email("");
        }
    }
}
