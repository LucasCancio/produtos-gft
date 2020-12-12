using ProdutosGFT.Domain.Entities;

namespace ProdutosGFT.Server.v2.DTOs.VendaDTOs
{
    public class ProdutoQtdViewDTO
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}