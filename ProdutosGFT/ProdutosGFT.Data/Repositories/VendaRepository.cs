using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProdutosGFT.Domain.Interfaces.Repositories;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Domain.Util.Exceptions;
using System.Threading.Tasks;

namespace ProdutosGFT.Data.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly ProdutosGFTDbContext _context;
        public VendaRepository(ProdutosGFTDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Venda>> SelectAllAsync(Func<Venda, Object> orderByFilter, bool asc = true)
        {
            List<Venda> vendas = await _context.Vendas
                                                    .AsNoTracking()
                                                    .ToListAsync();

            if (asc) vendas = vendas.OrderBy(orderByFilter).ToList();
            else vendas = vendas.OrderByDescending(orderByFilter).ToList();

            return vendas;
        }
        public async Task<Venda> SelectByIdAsync(long id)
        {
            if (id <= 0) throw new InvalidEntityException(msg: $"O Id '{id}' da entidade 'Venda' está inválido!", field: $"VendaId");

            Venda venda = await _context.Vendas
                                    .Include(v => v.Cliente)
                                    .FirstOrDefaultAsync(e => e.Id == id);

            if (venda == null) throw new EntityNotFoundException($"A entidade 'Venda' com Id '{id}' não existe!");

            return venda;
        }

        public async Task<Venda> SelectByIdAsNoTrackingAsync(long id)
        {
            if (id <= 0) throw new InvalidEntityException(msg: $"O Id '{id}' da entidade 'Venda' está inválido!", field: $"VendaId");

            Venda venda = await _context.Vendas
                                    .Include(v => v.Cliente)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.Id == id);

            if (venda == null) throw new EntityNotFoundException($"A entidade 'Venda' com Id '{id}' não existe!");

            return venda;
        }

        public async Task<List<Venda>> SelectVendasByProdutoAsync(long produtoId)
        {
            if (produtoId <= 0) throw new InvalidEntityException(msg: $"O Id '{produtoId}' da entidade 'Produto' está inválido!", field: $"ProdutoId");

            var result = _context.ProdutosVendas
                                                .AsNoTracking()
                                                .Where(pv => pv.ProdutoId == produtoId)
                                                .Select(pv => pv.Venda);

            List<Venda> vendas = await Task.FromResult(result.ToList());

            return vendas;

        }

        public async Task<List<ProdutoVenda>> SelectProdutosVendasByVendaAsync(long vendaId)
        {
            if (vendaId <= 0) throw new InvalidEntityException(msg: $"O Id '{vendaId}' da entidade 'Venda' está inválido!", field: $"VendaId");

            var result = _context.ProdutosVendas
                                                .AsNoTracking()
                                                .Where(pv => pv.VendaId == vendaId)
                                                .Include(pv => pv.Produto);

            List<ProdutoVenda> produtosVenda = await Task.FromResult(result.ToList());

            return produtosVenda;
        }

        public async Task<List<Produto>> SelectProdutosByVendaAsync(long vendaId)
        {
            List<Produto> produtos = (await SelectProdutosVendasByVendaAsync(vendaId))
                                                                                      .Select(pv => pv.Produto)
                                                                                      .ToList();
            return produtos;
        }

        public async Task<Venda> SaveAsync(Venda venda, List<ProdutoVenda> produtosVenda)
        {
            if (venda == null) throw new InvalidEntityException(msg: $"A entidade 'ProdutoVenda' está inválida!", field: $"ProdutoVenda");
            if (produtosVenda == null || produtosVenda.Count == 0) throw new InvalidEntityException(msg: $"A lista de Produtos está vazia ou inválida!", field: $"ProdutosIds");

            EntityState state = EntityState.Added;

            _context.Entry(venda).State = state;

            await _context.SaveChangesAsync();

            produtosVenda.ForEach(pv =>
            {
                pv.VendaId = venda.Id;

                _context.ProdutosVendas.Add(pv);
            });

            await _context.SaveChangesAsync();

            return venda;
        }

        public async Task DeleteByVendaIdAsync(long vendaId)
        {
            Venda venda = await SelectByIdAsync(vendaId);

            List<ProdutoVenda> produtoVendas = _context.ProdutosVendas
                                                        .Where(pv => pv.VendaId == vendaId)
                                                        .ToList();

            _context.ProdutosVendas.RemoveRange(produtoVendas);
            _context.Vendas.Remove(venda);

            _context.SaveChanges();
        }


    }
}