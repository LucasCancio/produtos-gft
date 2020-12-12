using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Server.v1.Services.Hateoas;

namespace ProdutosGFT.Server.v1.DTOs.FornecedorDTOs
{
    public class FornecedorViewDTO
    {
        public Fornecedor Fornecedor { get; set; }
        public Link[] Links { get; set; }
    }
}