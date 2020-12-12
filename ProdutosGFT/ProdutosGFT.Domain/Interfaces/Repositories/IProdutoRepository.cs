using ProdutosGFT.Domain.Interfaces.Generics.Repositories;
using ProdutosGFT.Domain.Entities;
using System.Collections.Generic;

namespace ProdutosGFT.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IDefaultRepository<Produto>
    {
    }
}