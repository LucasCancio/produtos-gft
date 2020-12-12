using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProdutosGFT.Data.Repositories.Generics;
using ProdutosGFT.Domain.Interfaces.Repositories;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Domain.Util.Exceptions;
using System.Threading.Tasks;

namespace ProdutosGFT.Data.Repositories
{
    public class ProdutoRepository : DefaultRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ProdutosGFTDbContext context) : base(context)
        {
        }

        public override async Task<List<Produto>> SelectAllAsync(Func<Produto, Object> orderByFilter, bool asc = true)
        {
            List<Produto> produtos = null;

            produtos = await _context.Produtos
                                      .Include(p => p.Fornecedor)
                                      .AsNoTracking()
                                      .ToListAsync();

            if (asc) produtos = produtos.OrderBy(orderByFilter).ToList();
            else produtos = produtos.OrderByDescending(orderByFilter).ToList();

            return produtos;
        }

        public override async Task<Produto> SelectByIdAsync(long id, bool onlyAtivos = false)
        {
            if (id <= 0) throw new InvalidEntityException(msg: $"O Id '{id}' da entidade 'Produto' está inválido!", field: $"ProdutoId");

            var fornecedor = await _context.Produtos
                                            .Include(p => p.Fornecedor)
                                            .FirstOrDefaultAsync(p => p.Id == id && (onlyAtivos ? p.IsAtivo : true));

            if (fornecedor == null) throw new EntityNotFoundException($"A entidade 'Produto' com Id '{id}' não existe ou está inativa!");

            return fornecedor;
        }
    }
}