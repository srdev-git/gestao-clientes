using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SRDev.GestaoClientes.Application.Clientes.Commands;
using SRDev.GestaoClientes.Application.Commands.Cliente;
using SRDev.GestaoClientes.Application.Queries.Cliente;
using System;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCliente([FromBody] CriarClienteCommand command)
        {
            var clienteId = await _mediator.Send(command);
            return CreatedAtAction(nameof(ObterClientePorId), new { id = clienteId }, clienteId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePorId(Guid id)
        {
            var cliente = await _mediator.Send(new ObterClientePorIdQuery { ClienteId = id });

            if (cliente == null) return NotFound();

            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverCliente(Guid id)
        {
            await _mediator.Send(new RemoverClienteCommand(id));
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(Guid id, [FromBody] AtualizarClienteCommand command)
        {
            if (id != command.Id) return BadRequest();

            var sucesso = await _mediator.Send(command);
            return sucesso ? NoContent() : NotFound();
        }

        [HttpPost("upload/{clienteId}")]
        public async Task<IActionResult> UploadImagem(Guid clienteId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var command = new UploadImagemCommand(clienteId, file.FileName, memoryStream.ToArray(), file.ContentType);
            var urlImagem = await _mediator.Send(command);

            return Ok(new { Mensagem = "Upload realizado com sucesso!", UrlImagem = urlImagem });
        }

        [HttpGet("paginado")]
        public async Task<IActionResult> ObterClientesPaginados([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new ObterClientesPaginadosQuery { PageNumber = pageNumber, PageSize = pageSize };
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }
    }
}
