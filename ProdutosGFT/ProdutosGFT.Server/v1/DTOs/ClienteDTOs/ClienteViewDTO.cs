using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Server.v1.Services.Hateoas;

namespace ProdutosGFT.Server.v1.DTOs.ClienteDTOs
{
    public class ClienteViewDTO
    {
        public Cliente Cliente { get; set; }
        public Link[] Links { get; set; }
    }
}