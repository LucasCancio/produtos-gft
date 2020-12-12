using Microsoft.AspNetCore.Mvc;
using ProdutosGFT.Domain.Interfaces.Generics.Entities;
using ProdutosGFT.Server.v1.Services.Hateoas;

namespace ProdutosGFT.Server.v1.Interfaces.Services
{
    public interface IHateoasService<TController> where TController : ControllerBase
    {
        void AddAction<TEntity>(string rel, string method) where TEntity : IDefaultEntity;
        Link[] GetActions(string suffix);
        Link[] GetActions(long id);
    }
}