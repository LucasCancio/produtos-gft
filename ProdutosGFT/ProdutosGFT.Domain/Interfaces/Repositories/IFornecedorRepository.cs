using System;
using System.Collections.Generic;
using ProdutosGFT.Domain.Interfaces.Generics.Repositories;
using ProdutosGFT.Domain.Entities;
using System.Threading.Tasks;

namespace ProdutosGFT.Domain.Interfaces.Repositories
{
    public interface IFornecedorRepository : IDefaultRepository<Fornecedor>
    {
        Task<List<Fornecedor>> SelectAllWithProdutosAsync(Func<Fornecedor, Object> orderByFilter, bool asc = true);
        Task<Fornecedor> SelectByIdWithProdutosAsync(long id);
    }
}