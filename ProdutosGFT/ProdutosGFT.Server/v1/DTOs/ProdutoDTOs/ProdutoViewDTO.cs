using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Server.v1.Services.Hateoas;

namespace ProdutosGFT.Server.v1.DTOs.ProdutoDTOs
{
    public class ProdutoViewDTO
    {
        public Produto Produto { get; set; }
        public Link[] Links { get; set; }
    }
}