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
    public class FornecedorRepository : DefaultRepository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(ProdutosGFTDbContext context) : base(context)
        {
        }

        public async Task<List<Fornecedor>> SelectAllWithProdutosAsync(Func<Fornecedor, Object> orderByFilter, bool asc = true)
        {
            List<Fornecedor> fornecedores = null;

            fornecedores = await _context.Fornecedores
                                                .Include(f => f.Produtos)
                                                .AsNoTracking()
                                                .Where(f => f.IsAtivo)
                                                .ToListAsync();

            if (asc) fornecedores = fornecedores.OrderBy(orderByFilter).ToList();
            else fornecedores = fornecedores.OrderByDescending(orderByFilter).ToList();

            return fornecedores;
        }

        public async Task<Fornecedor> SelectByIdWithProdutosAsync(long id)
        {
            if (id <= 0) throw new InvalidEntityException(msg: $"O Id '{id}' da entidade 'Fornecedor' está inválido!", field: "FornecedorId");

            var fornecedor = await _context.Fornecedores
                                            .Include(f => f.Produtos)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(f => f.Id == id && f.IsAtivo);

            if (fornecedor == null) throw new EntityNotFoundException($"A entidade 'Fornecedor' com Id '{id}' não existe ou está inativa!");

            return fornecedor;
        }

    }
}