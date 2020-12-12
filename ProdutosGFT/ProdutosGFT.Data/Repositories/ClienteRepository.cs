using ProdutosGFT.Data.Repositories.Generics;
using ProdutosGFT.Domain.Interfaces.Repositories;
using ProdutosGFT.Domain.Entities;

namespace ProdutosGFT.Data.Repositories
{
    public class ClienteRepository: DefaultRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ProdutosGFTDbContext context) : base(context)
        {
        }
    }
}