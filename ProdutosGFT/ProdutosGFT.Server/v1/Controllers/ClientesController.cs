using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutosGFT.Server.v1.Interfaces.Services;
using ProdutosGFT.Server.v1.DTOs.ClienteDTOs;
using ProdutosGFT.Domain.Util.Exceptions;
using ProdutosGFT.Server.v1.DTOs;
using System.Linq;
using System.Net.Http;
using ProdutosGFT.Domain.Entities;
using System.Threading.Tasks;
using ProdutosGFT.Domain.Util.Pagination;

namespace ProdutosGFT.Server.v1.Controllers
{
    [ApiVersion("1", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    [Produces(contentType: "application/json")]
    public class ClientesController : ControllerBase
    {
        protected readonly IClienteService _service;
        protected readonly IHateoasService<ClientesController> _hateoas;
        public ClientesController(IClienteService service, IHateoasService<ClientesController> hateoas)
        {
            this._service = service;
            this._hateoas = hateoas;

            this._hateoas.AddAction<Cliente>("Self", HttpMethod.Get.Method);
            this._hateoas.AddAction<Cliente>("Delete", HttpMethod.Delete.Method);
            this._hateoas.AddAction<Cliente>("Edit", HttpMethod.Put.Method);
            this._hateoas.AddAction<Cliente>("Partial Edit", HttpMethod.Patch.Method);
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="dto">Email e senha do cliente.</param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(SuccessDTO<TokenDTO>), statusCode: 200)]
        [AllowAnonymous]
        public virtual async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                TokenDTO tokenDTO = await _service.LoginAsync(dto);

                return Ok(new SuccessDTO<TokenDTO>(result: tokenDTO));
            }
            catch (InvalidEntityException ex)
            {
                List<ErrorFieldDTO> errorFields = ex.ErrorFields.Select(ef =>
                {
                    return new ErrorFieldDTO()
                    {
                        Field = ef.Key,
                        Error = ef.Value
                    };
                }).ToList();

                return BadRequest(new BadRequestDTO(errorFields));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundDTO(message: ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new InternalErrorDTO(message: $"Ocorreu um erro inesperado! {ex.Message}"));
            }
        }


        /// <summary>
        /// Buscar por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(SuccessDTO<ClienteViewDTO>), statusCode: 200)]
        public virtual async Task<IActionResult> Get(long id)
        {
            try
            {
                ClienteViewDTO viewDTO = await _service.GetByIdAsync(id);
                viewDTO.Links = _hateoas.GetActions(id);

                return Ok(new SuccessDTO<ClienteViewDTO>(result: viewDTO));
            }
            catch (InvalidEntityException ex)
            {
                List<ErrorFieldDTO> errorFields = ex.ErrorFields.Select(ef =>
                {
                    return new ErrorFieldDTO()
                    {
                        Field = ef.Key,
                        Error = ef.Value
                    };
                }).ToList();

                return BadRequest(new BadRequestDTO(errorFields));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundDTO(message: ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new InternalErrorDTO(message: $"Ocorreu um erro inesperado! {ex.Message}"));
            }
        }

        /// <summary>
        /// Listar todos os clientes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(SuccessDTO<List<ClienteViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAll([FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<ClienteViewDTO> clientes = await _service.GetAllAsync(pagination);
                clientes.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Cliente.Id);
                });

                return Ok(new SuccessDTO<List<ClienteViewDTO>>(result: clientes));
            }
            catch (InvalidEntityException ex)
            {
                List<ErrorFieldDTO> errorFields = ex.ErrorFields.Select(ef =>
                {
                    return new ErrorFieldDTO()
                    {
                        Field = ef.Key,
                        Error = ef.Value
                    };
                }).ToList();

                return BadRequest(new BadRequestDTO(errorFields));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundDTO(message: ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new InternalErrorDTO(message: $"Ocorreu um erro inesperado! {ex.Message}"));
            }
        }

        /// <summary>
        /// Busca clientes por nome.
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet("nome/{nome}")]
        [ProducesResponseType(typeof(SuccessDTO<List<ClienteViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAllByNome(string nome, [FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<ClienteViewDTO> clientes = await _service.GetByNomeAsync(nome, pagination);
                clientes.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Cliente.Id);
                });

                return Ok(new SuccessDTO<List<ClienteViewDTO>>(result: clientes));
            }
            catch (InvalidEntityException ex)
            {
                List<ErrorFieldDTO> errorFields = ex.ErrorFields.Select(ef =>
                {
                    return new ErrorFieldDTO()
                    {
                        Field = ef.Key,
                        Error = ef.Value
                    };
                }).ToList();

                return BadRequest(new BadRequestDTO(errorFields));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundDTO(message: ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new InternalErrorDTO(message: $"Ocorreu um erro inesperado! {ex.Message}"));
            }
        }

        /// <summary>
        /// Lista os clientes em ordém alfabética crescente, por nome.
        /// </summary>
        /// <returns></returns>
        [HttpGet("asc")]
        [ProducesResponseType(typeof(SuccessDTO<List<ClienteViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAllAscToName([FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<ClienteViewDTO> clientes = await _service.GetAllAscToNameAsync(pagination);
                clientes.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Cliente.Id);
                });

                return Ok(new SuccessDTO<List<ClienteViewDTO>>(result: clientes));
            }
            catch (InvalidEntityException ex)
            {
                List<ErrorFieldDTO> errorFields = ex.ErrorFields.Select(ef =>
                {
                    return new ErrorFieldDTO()
                    {
                        Field = ef.Key,
                        Error = ef.Value
                    };
                }).ToList();

                return BadRequest(new BadRequestDTO(errorFields));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundDTO(message: ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new InternalErrorDTO(message: $"Ocorreu um erro inesperado! {ex.Message}"));
            }
        }

        /// <summary>
        /// Lista os clientes em ordém alfabética decrescente, por nome.
        /// </summary>
        /// <returns></returns>
        [HttpGet("desc")]
        [ProducesResponseType(typeof(SuccessDTO<List<ClienteViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAllDescToName([FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<ClienteViewDTO> clientes = await _service.GetAllDescToNameAsync(pagination);
                clientes.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Cliente.Id);
                });
                return Ok(new SuccessDTO<List<ClienteViewDTO>>(result: clientes));
            }
            catch (InvalidEntityException ex)
            {
                List<ErrorFieldDTO> errorFields = ex.ErrorFields.Select(ef =>
                {
                    return new ErrorFieldDTO()
                    {
                        Field = ef.Key,
                        Error = ef.Value
                    };
                }).ToList();

                return BadRequest(new BadRequestDTO(errorFields));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundDTO(message: ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new InternalErrorDTO(message: $"Ocorreu um erro inesperado! {ex.Message}"));
            }
        }

        /// <summary>
        /// Inserir cliente.
        /// </summary>
        /// <param name="dto">ClienteViewDTO para inserir.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessDTO<ClienteViewDTO>), statusCode: 201)]
        public virtual async Task<IActionResult> Post([FromBody] ClienteCreateDTO dto)
        {
            try
            {
                ClienteViewDTO viewDTO = await _service.InsertAsync(dto);
                viewDTO.Links = _hateoas.GetActions(viewDTO.Cliente.Id);

                return StatusCode(201, new SuccessDTO<ClienteViewDTO>(result: viewDTO, statusCode: 201));
            }
            catch (InvalidEntityException ex)
            {
                List<ErrorFieldDTO> errorFields = ex.ErrorFields.Select(ef =>
                {
                    return new ErrorFieldDTO()
                    {
                        Field = ef.Key,
                        Error = ef.Value
                    };
                }).ToList();

                return BadRequest(new BadRequestDTO(errorFields));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundDTO(message: ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new InternalErrorDTO(message: $"Ocorreu um erro inesperado! {ex.Message}"));
            }

        }

        /// <summary>
        /// Atualizar cliente.
        /// </summary>
        /// <param name="dto">ClienteViewDTO para atualizar.</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(SuccessDTO<ClienteViewDTO>), statusCode: 200)]
        public virtual async Task<IActionResult> Put([FromBody] ClienteUpdateDTO dto)
        {
            try
            {
                ClienteViewDTO viewDTO = await _service.UpdateAsync(dto);
                viewDTO.Links = _hateoas.GetActions(viewDTO.Cliente.Id);

                return Ok(new SuccessDTO<ClienteViewDTO>(result: viewDTO));
            }
            catch (InvalidEntityException ex)
            {
                List<ErrorFieldDTO> errorFields = ex.ErrorFields.Select(ef =>
                {
                    return new ErrorFieldDTO()
                    {
                        Field = ef.Key,
                        Error = ef.Value
                    };
                }).ToList();

                return BadRequest(new BadRequestDTO(errorFields));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundDTO(message: ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new InternalErrorDTO(message: $"Ocorreu um erro inesperado! {ex.Message}"));
            }

        }

        /// <summary>
        /// Atualizar cliente parcialmente.
        /// </summary>
        /// <param name="dto">ClienteViewDTO para atualizar.</param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(SuccessDTO<ClienteViewDTO>), statusCode: 200)]
        public virtual async Task<IActionResult> Patch([FromBody] ClienteUpdateDTO dto)
        {
            try
            {
                ClienteViewDTO viewDTO = await _service.PartialUpdateAsync(dto);
                viewDTO.Links = _hateoas.GetActions(viewDTO.Cliente.Id);

                return Ok(new SuccessDTO<ClienteViewDTO>(result: viewDTO));
            }
            catch (InvalidEntityException ex)
            {
                List<ErrorFieldDTO> errorFields = ex.ErrorFields.Select(ef =>
                {
                    return new ErrorFieldDTO()
                    {
                        Field = ef.Key,
                        Error = ef.Value
                    };
                }).ToList();

                return BadRequest(new BadRequestDTO(errorFields));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundDTO(message: ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new InternalErrorDTO(message: $"Ocorreu um erro inesperado! {ex.Message}"));
            }

        }

        /// <summary>
        /// Excluir cliente por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:long}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(typeof(SuccessDTO<string>), statusCode: 200)]
        public virtual async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _service.DeleteByIdAsync(id);
                return Ok(new SuccessDTO<string>(result: "Cliente excluído com sucesso!"));
            }
            catch (InvalidEntityException ex)
            {
                List<ErrorFieldDTO> errorFields = ex.ErrorFields.Select(ef =>
                {
                    return new ErrorFieldDTO()
                    {
                        Field = ef.Key,
                        Error = ef.Value
                    };
                }).ToList();

                return BadRequest(new BadRequestDTO(errorFields));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundDTO(message: ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new InternalErrorDTO(message: $"Ocorreu um erro inesperado! {ex.Message}"));
            }

        }

        /// <summary>
        ///  Desativar cliente por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("desative/{id:long}")]
        [ProducesResponseType(typeof(SuccessDTO<string>), statusCode: 200)]
        public virtual async Task<IActionResult> Desative(long id)
        {
            try
            {
                await _service.DesativeByIdAsync(id);
                return Ok(new SuccessDTO<string>(result: "Cliente desativado com sucesso!"));
            }
            catch (InvalidEntityException ex)
            {
                List<ErrorFieldDTO> errorFields = ex.ErrorFields.Select(ef =>
                {
                    return new ErrorFieldDTO()
                    {
                        Field = ef.Key,
                        Error = ef.Value
                    };
                }).ToList();

                return BadRequest(new BadRequestDTO(errorFields));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundDTO(message: ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new InternalErrorDTO(message: $"Ocorreu um erro inesperado! {ex.Message}"));
            }

        }

        /// <summary>
        ///  Ativar cliente por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("active/{id:long}")]
        [ProducesResponseType(typeof(SuccessDTO<string>), statusCode: 200)]
        public virtual async Task<IActionResult> Active(long id)
        {
            try
            {
                await _service.ActiveByIdAsync(id);
                return Ok(new SuccessDTO<string>(result: "Cliente ativado com sucesso!"));
            }
            catch (InvalidEntityException ex)
            {
                List<ErrorFieldDTO> errorFields = ex.ErrorFields.Select(ef =>
                {
                    return new ErrorFieldDTO()
                    {
                        Field = ef.Key,
                        Error = ef.Value
                    };
                }).ToList();

                return BadRequest(new BadRequestDTO(errorFields));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new NotFoundDTO(message: ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new InternalErrorDTO(message: $"Ocorreu um erro inesperado! {ex.Message}"));
            }

        }

    }
}