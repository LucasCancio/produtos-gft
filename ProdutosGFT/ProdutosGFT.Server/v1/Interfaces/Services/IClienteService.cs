using System.Collections.Generic;
using ProdutosGFT.Server.v1.DTOs.ClienteDTOs;
using ProdutosGFT.Domain.Entities;
using System.Threading.Tasks;
using ProdutosGFT.Domain.Util.Pagination;

namespace ProdutosGFT.Server.v1.Interfaces.Services
{
    public interface IClienteService
    {
        Task<PagedList<ClienteViewDTO>> GetAllAsync(PaginationParameters pagination);
        Task<PagedList<ClienteViewDTO>> GetAllAscToNameAsync(PaginationParameters pagination);
        Task<PagedList<ClienteViewDTO>> GetAllDescToNameAsync(PaginationParameters pagination);
        Task<PagedList<ClienteViewDTO>> GetByNomeAsync(string nome, PaginationParameters pagination);
        Task<ClienteViewDTO> GetByIdAsync(long id);
        Task<ClienteViewDTO> UpdateAsync(ClienteUpdateDTO dto);
        Task<ClienteViewDTO> PartialUpdateAsync(ClienteUpdateDTO dto);
        Task<ClienteViewDTO> InsertAsync(ClienteCreateDTO dto);
        Task DeleteByIdAsync(long id);
        Task DesativeByIdAsync(long id);
        Task ActiveByIdAsync(long id);
        Task<TokenDTO> LoginAsync(LoginDTO loginDTO);
    }
}