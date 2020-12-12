using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProdutosGFT.Domain.Entities;

namespace ProdutosGFT.Domain.Interfaces.Repositories
{
    public interface IVendaRepository
    {
        Task<List<Venda>> SelectAllAsync(Func<Venda, Object> orderByFilter, bool asc = true);
        Task<Venda> SelectByIdAsync(long id);
        Task<Venda> SelectByIdAsNoTrackingAsync(long id);
        Task<List<Venda>> SelectVendasByProdutoAsync(long produtoId);
        Task<List<Produto>> SelectProdutosByVendaAsync(long vendaId);
        Task<List<ProdutoVenda>> SelectProdutosVendasByVendaAsync(long vendaId);
        Task<Venda> SaveAsync(Venda venda, List<ProdutoVenda> produtosVenda); 
        Task DeleteByVendaIdAsync(long vendaId);
    }
}