using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutosGFT.Server.v1.Interfaces.Services;

namespace ProdutosGFT.Server.v2.Controllers
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    [Produces(contentType: "application/json")]
    public class ProdutosController : v1.Controllers.ProdutosController
    {
        public ProdutosController(IProdutoService service, IHateoasService<v1.Controllers.ProdutosController> hateoas) 
        : base(service, hateoas)
        {
        }
    }
}