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
    public class ClientesController : v1.Controllers.ClientesController
    {
        public ClientesController(IClienteService service, IHateoasService<v1.Controllers.ClientesController> hateoas) 
        : base(service, hateoas)
        {
        }
    }
}