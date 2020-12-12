using System.Threading.Tasks;
using ProdutosGFT.Domain.Util.Pagination;
using ProdutosGFT.Server.v2.DTOs.VendaDTOs;

namespace ProdutosGFT.Server.v2.Interfaces.Services
{
    public interface IVendaService
    {
        Task<PagedList<VendaViewDTO>> GetAllAsync(PaginationParameters pagination);
        Task<VendaViewDTO> GetByIdAsync(long vendaId);
        Task<VendaViewDTO> InsertAsync(VendaCreateDTO dto);
        Task DeleteByIdAsync(long vendaId);
    }
}