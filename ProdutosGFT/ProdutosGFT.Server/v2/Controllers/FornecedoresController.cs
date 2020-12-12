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
    public class FornecedoresController : v1.Controllers.FornecedoresController
    {
        public FornecedoresController(IFornecedorService service, IHateoasService<v1.Controllers.FornecedoresController> hateoas) 
        : base(service, hateoas)
        {
        }
    }
}