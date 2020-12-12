using System.Collections.Generic;
using ProdutosGFT.Server.v1.DTOs.FornecedorDTOs;
using ProdutosGFT.Domain.Entities;
using System.Threading.Tasks;
using ProdutosGFT.Domain.Util.Pagination;

namespace ProdutosGFT.Server.v1.Interfaces.Services
{
    public interface IFornecedorService
    {
        Task<PagedList<FornecedorViewDTO>> GetAllAsync(PaginationParameters pagination);
        Task<PagedList<FornecedorViewDTO>> GetAllAscToNameAsync(PaginationParameters pagination);
        Task<PagedList<FornecedorViewDTO>> GetAllDescToNameAsync(PaginationParameters pagination);
        Task<PagedList<FornecedorViewDTO>> GetByNomeAsync(string nome,PaginationParameters pagination);
        Task<FornecedorViewDTO> GetByIdAsync(long id);
        Task<FornecedorViewDTO> GetByIdWithProdutosAsync(long id);
        Task<FornecedorViewDTO> UpdateAsync(FornecedorUpdateDTO dto);
        Task<FornecedorViewDTO> PartialUpdateAsync(FornecedorUpdateDTO dto);
        Task<FornecedorViewDTO> InsertAsync(FornecedorCreateDTO dto);
        Task DeleteByIdAsync(long id);
        Task DesativeByIdAsync(long id);
        Task ActiveByIdAsync(long id);
    }
}