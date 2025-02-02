using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SRDev.GestaoClientes.Domain.UsuarioAggregate;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Application.Autenticacao
{
    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public LoginHandler(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);

            if (usuario == null || usuario.SenhaHash != request.Senha)
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            return GerarTokenJWT(usuario.Email);
        }

        private string GerarTokenJWT(string email)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"] ?? string.Empty);
            var audience = _configuration["JwtSettings:Audience"];
            var issuer = _configuration["JwtSettings:Issuer"];

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = audience,
                Issuer = issuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
