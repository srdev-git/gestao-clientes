using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Infrastructure.Auth
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string userName);
    }
}
