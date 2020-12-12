using System.Collections.Generic;
using ProdutosGFT.Server.v1.DTOs.VendaDTOs;
using System.Threading.Tasks;
using ProdutosGFT.Domain.Util.Pagination;

namespace ProdutosGFT.Server.v1.Interfaces.Services
{
    public interface IVendaService
    {
        Task<PagedList<VendaViewDTO>> GetAllAsync(PaginationParameters pagination);
        Task<VendaViewDTO> GetByIdAsync(long vendaId);
        Task<VendaViewDTO> InsertAsync(VendaCreateDTO dto);
        Task DeleteByIdAsync(long vendaId);
    }
}