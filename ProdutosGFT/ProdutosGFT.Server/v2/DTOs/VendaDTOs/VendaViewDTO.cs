using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Server.v1.Services.Hateoas;

namespace ProdutosGFT.Server.v2.DTOs.VendaDTOs
{
    public class VendaViewDTO
    {
        public Venda Venda { get; set; }
        public ProdutoQtdViewDTO[] Produtos { get; set; }
        public Link[] Links { get; set; }
    }
}