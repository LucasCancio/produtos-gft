using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutosGFT.Server.v1.DTOs.ProdutoDTOs;
using ProdutosGFT.Server.v1.Interfaces.Services;
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
    public class ProdutosController : ControllerBase
    {
        protected readonly IProdutoService _service;
        protected readonly IHateoasService<ProdutosController> _hateoas;
        public ProdutosController(IProdutoService service, IHateoasService<ProdutosController> hateoas)
        {
            this._service = service;
            this._hateoas = hateoas;

            this._hateoas.AddAction<Produto>("Self", HttpMethod.Get.Method);
            this._hateoas.AddAction<Produto>("Delete", HttpMethod.Delete.Method);
            this._hateoas.AddAction<Produto>("Edit", HttpMethod.Put.Method);
            this._hateoas.AddAction<Produto>("Partial Edit", HttpMethod.Patch.Method);
        }

        /// <summary>
        /// Buscar por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(SuccessDTO<ProdutoViewDTO>), statusCode: 200)]
        public virtual async Task<IActionResult> Get(long id)
        {
            try
            {
                ProdutoViewDTO viewDTO = await _service.GetByIdAsync(id);
                viewDTO.Links = _hateoas.GetActions(id);

                return Ok(new SuccessDTO<ProdutoViewDTO>(result: viewDTO));
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
        /// Listar todos os produtos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(SuccessDTO<List<ProdutoViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAll([FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<ProdutoViewDTO> produtos = await _service.GetAllAsync(pagination);
                produtos.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Produto.Id);
                });

                return Ok(new SuccessDTO<List<ProdutoViewDTO>>(result: produtos));
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
        /// Busca produtos por nome.
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet("nome/{nome}")]
        [ProducesResponseType(typeof(SuccessDTO<List<ProdutoViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAllByNome(string nome, [FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<ProdutoViewDTO> produtos = await _service.GetByNomeAsync(nome, pagination);
                produtos.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Produto.Id);
                });

                return Ok(new SuccessDTO<List<ProdutoViewDTO>>(result: produtos));
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
        /// Lista os produtos em ordém alfabética crescente, por nome.
        /// </summary>
        /// <returns></returns>
        [HttpGet("asc")]
        [ProducesResponseType(typeof(SuccessDTO<List<ProdutoViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAllAscToName([FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<ProdutoViewDTO> produtos = await _service.GetAllAscToNameAsync(pagination);
                produtos.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Produto.Id);
                });

                return Ok(new SuccessDTO<List<ProdutoViewDTO>>(result: produtos));
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
        /// Lista os produtos em ordém alfabética decrescente, por nome.
        /// </summary>
        /// <returns></returns>
        [HttpGet("desc")]
        [ProducesResponseType(typeof(SuccessDTO<List<ProdutoViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAllDescToName([FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<ProdutoViewDTO> produtos = await _service.GetAllDescToNameAsync(pagination);
                produtos.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Produto.Id);
                });

                return Ok(new SuccessDTO<List<ProdutoViewDTO>>(result: produtos));
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
        /// Inserir produto.
        /// </summary>
        /// <param name="dto">ProdutoViewDTO para inserir.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessDTO<ProdutoViewDTO>), statusCode: 200)]
        public virtual async Task<IActionResult> Post([FromBody] ProdutoCreateDTO dto)
        {
            try
            {
                ProdutoViewDTO viewDTO = await _service.InsertAsync(dto);
                viewDTO.Links = _hateoas.GetActions(viewDTO.Produto.Id);

                return StatusCode(201, new SuccessDTO<ProdutoViewDTO>(result: viewDTO, statusCode: 201));
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
        /// Atualizar produto.
        /// </summary>
        /// <param name="dto">ProdutoViewDTO para atualizar.</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(SuccessDTO<ProdutoViewDTO>), statusCode: 201)]
        public virtual async Task<IActionResult> Put([FromBody] ProdutoUpdateDTO dto)
        {
            try
            {
                ProdutoViewDTO viewDTO = await _service.UpdateAsync(dto);
                viewDTO.Links = _hateoas.GetActions(viewDTO.Produto.Id);

                return Ok(new SuccessDTO<ProdutoViewDTO>(result: viewDTO));
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
        /// Atualizar produto parcialmente.
        /// </summary>
        /// <param name="dto">ProdutoViewDTO para atualizar.</param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(SuccessDTO<ProdutoViewDTO>), statusCode: 200)]
        public virtual async Task<IActionResult> Patch([FromBody] ProdutoUpdateDTO dto)
        {
            try
            {
                if (dto.Id <= 0) throw new InvalidEntityException(msg: $"O Id '{dto.Id}' da entidade 'ProdutoViewDTO' está inválido!", field: $"ProdutoId");

                ProdutoViewDTO viewDTO = await _service.PartialUpdateAsync(dto);
                viewDTO.Links = _hateoas.GetActions(viewDTO.Produto.Id);

                return Ok(new SuccessDTO<ProdutoViewDTO>(result: viewDTO));
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
        /// Excluir produto por Id.
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
                return Ok(new SuccessDTO<string>(result: "Produto excluído com sucesso!"));
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
        ///  Desativar produto por Id.
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
                return Ok(new SuccessDTO<string>(result: "Produto desativado com sucesso!"));
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
        ///  Ativar produto por Id.
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
                return Ok(new SuccessDTO<string>(result: "Produto ativado com sucesso!"));
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