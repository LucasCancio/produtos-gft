using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutosGFT.Server.v1.DTOs.VendaDTOs;
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
    public class VendasController : ControllerBase
    {
        private readonly IVendaService _service;
        protected readonly IHateoasService<VendasController> _hateoas;
        public VendasController(IVendaService service, IHateoasService<VendasController> hateoas)
        {
            this._service = service;
            this._hateoas = hateoas;

            this._hateoas.AddAction<Venda>("Self", HttpMethod.Get.Method);
            this._hateoas.AddAction<Venda>("Delete", HttpMethod.Delete.Method);
        }

        /// <summary>
        /// Buscar por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(SuccessDTO<VendaViewDTO>), statusCode: 200)]
        public virtual async Task<IActionResult> Get(long id)
        {
            try
            {
                VendaViewDTO viewDTO = await _service.GetByIdAsync(id);
                viewDTO.Links = _hateoas.GetActions(viewDTO.Venda.Id);

                return Ok(new SuccessDTO<VendaViewDTO>(result: viewDTO));
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
        /// Listar todas as vendas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(SuccessDTO<List<VendaViewDTO>>), statusCode: 200)]
        public virtual async Task<IActionResult> GetAll([FromQuery] PaginationParameters pagination)
        {
            try
            {
                List<VendaViewDTO> vendasDTO = await _service.GetAllAsync(pagination);
                vendasDTO.ForEach(dto =>
                {
                    dto.Links = _hateoas.GetActions(dto.Venda.Id);
                });


                return Ok(new SuccessDTO<List<VendaViewDTO>>(result: vendasDTO));
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
        /// Inserir venda.
        /// </summary>
        /// <param name="dto">Venda para inserir.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessDTO<VendaViewDTO>), statusCode: 201)]
        public virtual async Task<IActionResult> Post([FromBody] VendaCreateDTO dto)
        {
            try
            {
                VendaViewDTO viewDTO = await _service.InsertAsync(dto);
                viewDTO.Links = _hateoas.GetActions(viewDTO.Venda.Id);

                return StatusCode(201, new SuccessDTO<VendaViewDTO>(result: viewDTO, statusCode: 201));
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
        /// Excluir venda por Id.
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
                return Ok(new SuccessDTO<string>(result: "Venda excluída com sucesso!"));
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