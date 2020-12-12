using System.Collections.Generic;
using System.Linq;
using ProdutosGFT.Server.v1.DTOs.VendaDTOs;
using ProdutosGFT.Domain.Interfaces.Repositories;
using ProdutosGFT.Server.v1.Interfaces.Services;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Domain.Util.Exceptions;
using System.Threading.Tasks;
using ProdutosGFT.Domain.Util.Pagination;

namespace ProdutosGFT.Server.v1.Services
{
    public class VendaService : IVendaService
    {
        protected readonly IVendaRepository _vendaRepository;
        protected readonly IProdutoRepository _produtoRepository;
        protected readonly IFornecedorRepository _fornecedorRepository;
        protected readonly IClienteRepository _clienteRepository;

        public VendaService(IVendaRepository vendaRepository,
         IProdutoRepository produtoRepository,
         IFornecedorRepository fornecedorRepository,
         IClienteRepository clienteRepository)
        {
            this._vendaRepository = vendaRepository;
            this._produtoRepository = produtoRepository;
            this._fornecedorRepository = fornecedorRepository;
            this._clienteRepository = clienteRepository;
        }


        public virtual async Task DeleteByIdAsync(long vendaId)
        {
            await _vendaRepository.DeleteByVendaIdAsync(vendaId);
        }

        public virtual async Task<PagedList<VendaViewDTO>> GetAllAsync(PaginationParameters pagination)
        {
            var vendasViews = new List<VendaViewDTO>();
            List<Venda> vendas = await _vendaRepository.SelectAllAsync(orderByFilter: venda => venda.DataCompra);

            vendas.ForEach(async (venda) =>
            {
                List<Produto> produtos = await _vendaRepository.SelectProdutosByVendaAsync(venda.Id);

                vendasViews.Add(new VendaViewDTO()
                {
                    Produtos = produtos,
                    Venda = venda
                });
            });

            return PagedList<VendaViewDTO>.ToPagedList(vendasViews, pagination.pageNumber, pagination.pageSize);
        }

        public virtual async Task<VendaViewDTO> GetByIdAsync(long vendaId)
        {
            List<Produto> produtos = await _vendaRepository.SelectProdutosByVendaAsync(vendaId);
            Venda venda = await _vendaRepository.SelectByIdAsNoTrackingAsync(vendaId);

            return new VendaViewDTO()
            {
                Produtos = produtos,
                Venda = venda
            };
        }

        public virtual async Task<VendaViewDTO> InsertAsync(VendaCreateDTO dto)
        {
            if (!dto.IsValid()) throw new InvalidEntityException(dto.Errors);

            await dto.FixDTO(_clienteRepository, _produtoRepository);

            List<long> fornecedoresIdsDosProdutos = dto.Produtos
                                                    .Select(p => p.FornecedorId)
                                                    .Distinct()
                                                    .ToList();

            bool notSameSize = fornecedoresIdsDosProdutos.Count != dto.FornecedoresIds.Count;
            bool notSameIds = !(fornecedoresIdsDosProdutos.Intersect(dto.FornecedoresIds).Any());
            bool fornecedoresNotEqual = notSameSize || notSameIds;

            if (fornecedoresNotEqual)
                throw new InvalidEntityException(msg: $"Fornecedores da entidade 'Venda' estão inválidos!", field: $"FornecedoresIds");

            List<ProdutoVenda> produtosVenda = dto.Produtos
                                                           .Select(produto => new ProdutoVenda(produtoId: produto.Id))
                                                           .ToList();

            dto.Produtos.ForEach(p => p.ChangeStock());

            Venda venda = await _vendaRepository.SaveAsync(dto.ToModel(), produtosVenda);

            var viewDTO = new VendaViewDTO()
            {
                Venda = venda,
                Produtos = dto.Produtos,
            };

            return viewDTO;
        }

    }
}