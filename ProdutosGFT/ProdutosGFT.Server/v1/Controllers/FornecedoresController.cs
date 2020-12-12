using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutosGFT.Server.v1.DTOs.FornecedorDTOs;
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
    public class FornecedoresController : ControllerBase
    {
        protected readonly IFornecedorService _service;
        protected readonly IHateoasService<FornecedoresController> _hateoas;
        public FornecedoresController(IFornecedorService service, IHateoasService<FornecedoresController> hateoas)
        {
            this._service = service;
            this._hateoas = hateoas;

            this._hateoas.AddAction<Fornecedor>("Self", HttpMethod.Get.Method);
            this._hateoas.AddAction<Fornecedor>("Delete", HttpMethod.Delete.Method);
            this._hateoas.AddAction<Fornecedor>("Edit", HttpMethod.Put.Method);
            this._hateoas.AddAction<Fornecedor>("Partial Edit", HttpMethod.Patch.Method);
        }

        /// <summary>
        /// Buscar por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(SuccessDTO<FornecedorViewDTO>), statusCode: 200)]
        public virtual async Task<IActionResult> Get(long id)
        {
            try
            {
                FornecedorViewDTO viewDTO = await _service.GetByIdWithProdutosAsync(id);
                viewDTO.Links = _hateoas.GetActions(viewDTO.Fornecedor.Id);

                return Ok(new SuccessDTO<FornecedorViewDTO>(result: viewDTO));
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
        /// Listar todos os fornecedores.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(SuccessDTO<List<FornecedorViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAll([FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<FornecedorViewDTO> fornecedores = await _service.GetAllAsync(pagination);
                fornecedores.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Fornecedor.Id);
                });

                return Ok(new SuccessDTO<List<FornecedorViewDTO>>(result: fornecedores));
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
        /// Busca fornecedores por nome.
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet("nome/{nome}")]
        [ProducesResponseType(typeof(SuccessDTO<List<FornecedorViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAllByNome(string nome, [FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<FornecedorViewDTO> fornecedores = await _service.GetByNomeAsync(nome, pagination);
                fornecedores.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Fornecedor.Id);
                });

                return Ok(new SuccessDTO<List<FornecedorViewDTO>>(result: fornecedores));
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
        /// Lista os fornecedores em ordém alfabética crescente, por nome.
        /// </summary>
        /// <returns></returns>
        [HttpGet("asc")]
        [ProducesResponseType(typeof(SuccessDTO<List<FornecedorViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAllAscToName([FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<FornecedorViewDTO> fornecedores = await _service.GetAllAscToNameAsync(pagination);
                fornecedores.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Fornecedor.Id);
                });

                return Ok(new SuccessDTO<List<FornecedorViewDTO>>(result: fornecedores));
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
        /// Lista os fornecedores em ordém alfabética decrescente, por nome.
        /// </summary>
        /// <returns></returns>
        [HttpGet("desc")]
        [ProducesResponseType(typeof(SuccessDTO<List<FornecedorViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAllDescToName([FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<FornecedorViewDTO> fornecedores = await _service.GetAllDescToNameAsync(pagination);
                fornecedores.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Fornecedor.Id);
                });

                return Ok(new SuccessDTO<List<FornecedorViewDTO>>(result: fornecedores));
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
        /// Inserir fornecedor.
        /// </summary>
        /// <param name="dto">FornecedorViewDTO para inserir.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessDTO<FornecedorViewDTO>), statusCode: 201)]
        public virtual async Task<IActionResult> Post([FromBody] FornecedorCreateDTO dto)
        {
            try
            {
                FornecedorViewDTO viewDTO = await _service.InsertAsync(dto);
                viewDTO.Links = _hateoas.GetActions(viewDTO.Fornecedor.Id);

                return StatusCode(201, new SuccessDTO<FornecedorViewDTO>(result: viewDTO, statusCode: 201));
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
        /// Atualizar fornecedor.
        /// </summary>
        /// <param name="dto">FornecedorViewDTO para atualizar.</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(SuccessDTO<FornecedorViewDTO>), statusCode: 200)]
        public virtual async Task<IActionResult> Put([FromBody] FornecedorUpdateDTO dto)
        {
            try
            {
                FornecedorViewDTO viewDTO = await _service.UpdateAsync(dto);
                viewDTO.Links = _hateoas.GetActions(viewDTO.Fornecedor.Id);

                return Ok(new SuccessDTO<FornecedorViewDTO>(result: viewDTO));
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
        /// Atualizar fornecedor parcialmente.
        /// </summary>
        /// <param name="dto">FornecedorViewDTO para atualizar.</param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(SuccessDTO<FornecedorViewDTO>), statusCode: 200)]
        public virtual async Task<IActionResult> Patch([FromBody] FornecedorUpdateDTO dto)
        {
            try
            {
                if (dto.Id <= 0) throw new InvalidEntityException(msg: $"O Id '{dto.Id}' da entidade 'FornecedorViewDTO' está inválido!", field: $"FornecedorId");

                FornecedorViewDTO viewDTO = await _service.PartialUpdateAsync(dto);
                viewDTO.Links = _hateoas.GetActions(viewDTO.Fornecedor.Id);

                return Ok(new SuccessDTO<FornecedorViewDTO>(result: viewDTO));
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
        /// Excluir fornecedor por Id.
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
                return Ok(new SuccessDTO<string>(result: "Fornecedor excluído com sucesso!"));
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
        ///  Desativar fornecedor por Id.
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
                return Ok(new SuccessDTO<string>(result: "Fornecedor desativado com sucesso!"));
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
        ///  Ativar fornecedor por Id.
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
                return Ok(new SuccessDTO<string>(result: "Fornecedor ativado com sucesso!"));
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