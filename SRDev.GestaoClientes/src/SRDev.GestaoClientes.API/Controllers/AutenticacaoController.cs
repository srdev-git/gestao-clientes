using MediatR;
using Microsoft.AspNetCore.Mvc;
using SRDev.GestaoClientes.Application.Autenticacao;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutenticacaoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(new { Token = token });
        }
    }
}
