using System.Collections.Generic;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Server.v1.Services.Hateoas;

namespace ProdutosGFT.Server.v1.DTOs.VendaDTOs
{
    public class VendaViewDTO
    {
        public Venda Venda { get; set; }
        public List<Produto> Produtos { get; set; }
        public Link[] Links { get; set; }
    }
}