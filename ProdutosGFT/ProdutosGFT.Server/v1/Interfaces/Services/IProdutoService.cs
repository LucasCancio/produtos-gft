using System.Collections.Generic;
using ProdutosGFT.Server.v1.DTOs.ProdutoDTOs;
using ProdutosGFT.Domain.Entities;
using System.Threading.Tasks;
using ProdutosGFT.Domain.Util.Pagination;

namespace ProdutosGFT.Server.v1.Interfaces.Services
{
    public interface IProdutoService
    {
        Task<PagedList<ProdutoViewDTO>> GetAllAsync(PaginationParameters pagination);
        Task<PagedList<ProdutoViewDTO>> GetAllAscToNameAsync(PaginationParameters pagination);
        Task<PagedList<ProdutoViewDTO>> GetAllDescToNameAsync(PaginationParameters pagination);
        Task<PagedList<ProdutoViewDTO>> GetByNomeAsync(string nome,PaginationParameters pagination);
        Task<ProdutoViewDTO> GetByIdAsync(long id);
        Task<ProdutoViewDTO> UpdateAsync(ProdutoUpdateDTO dto);
        Task<ProdutoViewDTO> PartialUpdateAsync(ProdutoUpdateDTO dto);
        Task<ProdutoViewDTO> InsertAsync(ProdutoCreateDTO dto);
        Task DeleteByIdAsync(long id);
        Task DesativeByIdAsync(long id);
        Task ActiveByIdAsync(long id);
    }
}