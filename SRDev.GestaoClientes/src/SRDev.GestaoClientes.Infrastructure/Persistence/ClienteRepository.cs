using Microsoft.EntityFrameworkCore;
using SRDev.GestaoClientes.Domain.ClienteAggregate;
using SRDev.GestaoClientes.Domain.Exceptions;
using SRDev.GestaoClientes.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDev.GestaoClientes.Infrastructure.Persistence
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly GestaoClientesDbContext _context;

        public ClienteRepository(GestaoClientesDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> ObterPorIdAsync(Guid id)
        {
            return await _context.Clientes.Include(c => c.Logradouros)
                                          .FirstOrDefaultAsync(c => c.Id == id);
        }

        //todo: refatorar solução para novos logradouros na atualização do cliente
        public async Task<bool> AtualizarAsync(Cliente cliente)
        {
            try
            {
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is Logradouro)
                    {
                        entry.State = EntityState.Added;
                    }
                }
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task RemoverAsync(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _context.Clientes.AnyAsync(c => c.Email == email);
        }

        public async Task<PaginacaoResultado<ClientePaginadoDto>> ObterClientesPaginadosAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Set<Cliente>()
                .OrderBy(c => c.Nome)
                .Select(c => new ClientePaginadoDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Email = c.Email,
                    Logotipo = c.Logotipo
                });

            int totalItens = await query.CountAsync(cancellationToken);
            var itens = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginacaoResultado<ClientePaginadoDto>
            {
                Itens = itens,
                TotalItens = totalItens,
                TotalPaginas = (int)Math.Ceiling(totalItens / (double)pageSize)
            };
        }
    }
}
