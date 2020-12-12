using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutosGFT.Domain.Interfaces.Generics.Entities;
using ProdutosGFT.Server.v1.Interfaces.Services;

namespace ProdutosGFT.Server.v1.Services.Hateoas
{
    public class HateoasService<TController> : IHateoasService<TController> where TController : ControllerBase
    {
        public HateoasService(string host, string protocol = "https")
        {
            string controllerName = typeof(TController).Name.Replace("Controller", "");
            this.url = $"{protocol}://{host}/api/{controllerName}";
        }

        public HateoasService(IHttpContextAccessor httpContextAccessor) :
        this(
                host: httpContextAccessor.HttpContext.Request.Host.Value,
                protocol: httpContextAccessor.HttpContext.Request.Scheme
            )
        { }

        public string url { get; private set; }
        public List<Link> actions = new List<Link>();


        public virtual void AddAction<TEntity>(string rel, string method) where TEntity : IDefaultEntity
        {
            string entityName = typeof(TEntity).Name;

            rel = $"{entityName}_{rel}";
            this.actions.Add(new Link(url, rel, method));
        }

        public virtual Link[] GetActions(string suffix)
        {
            Link[] tempLinks = new Link[actions.Count];

            for (int i = 0; i < tempLinks.Length; i++)
            {
                tempLinks[i] = new Link(actions[i].href, actions[i].rel, actions[i].method);
            }

            /* Montagem do link */
            foreach (Link link in tempLinks)
            {
                link.href = $"{link.href}/{suffix}";
            }

            return tempLinks;
        }

        public virtual Link[] GetActions(long id)
        {
            return GetActions(id.ToString());
        }


    }
}