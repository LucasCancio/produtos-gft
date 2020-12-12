using System.Text.Json.Serialization;
using ProdutosGFT.Domain.Entities;

namespace ProdutosGFT.Server.v2.DTOs.VendaDTOs
{
    public class ProdutoQtdCreateDTO
    {
        [JsonIgnore]
        public Produto Produto { get; set; }
        public long Id { get; set; }
        public int Quantidade { get; set; }

        public ProdutoQtdViewDTO ToViewDTO()
        {
            return new ProdutoQtdViewDTO()
            {
                Produto = this.Produto,
                Quantidade = this.Quantidade
            };
        }
    }
}