using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Domain.Interfaces.Repositories;
using ProdutosGFT.Domain.Util.Exceptions;
using ProdutosGFT.Domain.Util.Pagination;
using ProdutosGFT.Server.v2.DTOs.VendaDTOs;
using ProdutosGFT.Server.v2.Interfaces.Services;

namespace ProdutosGFT.Server.v2.Services
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

        public async Task<PagedList<VendaViewDTO>> GetAllAsync(PaginationParameters pagination)
        {
            var vendasViews = new List<VendaViewDTO>();
            List<Venda> vendas = await _vendaRepository.SelectAllAsync(orderByFilter: venda => venda.DataCompra);

            vendas.ForEach(async (venda) =>
            {
                List<ProdutoVenda> produtosVenda = await _vendaRepository.SelectProdutosVendasByVendaAsync(venda.Id);

                var dto = new VendaViewDTO()
                {
                    Venda = venda
                };

                InsertProdutosVendaInDTO(dto, produtosVenda);
                vendasViews.Add(dto);
            });

            return PagedList<VendaViewDTO>.ToPagedList(vendasViews, pagination.pageNumber, pagination.pageSize);
        }

        public async Task<VendaViewDTO> GetByIdAsync(long vendaId)
        {
            List<ProdutoVenda> produtosVenda = await _vendaRepository.SelectProdutosVendasByVendaAsync(vendaId);
            Venda venda = await _vendaRepository.SelectByIdAsNoTrackingAsync(vendaId);

            var dto = new VendaViewDTO()
            {
                Venda = venda
            };

            InsertProdutosVendaInDTO(dto, produtosVenda);
            return dto;
        }

        private void InsertProdutosVendaInDTO(VendaViewDTO dto, List<ProdutoVenda> produtosVenda)
        {
            var produtosQtd = new List<ProdutoQtdViewDTO>();

            produtosVenda.ForEach(pv =>
            {
                produtosQtd.Add(new ProdutoQtdViewDTO()
                {
                    Produto = pv.Produto,
                    Quantidade = pv.Quantidade
                });
            });

            dto.Produtos = produtosQtd.ToArray();
        }

        public async Task<VendaViewDTO> InsertAsync(VendaCreateDTO dto)
        {
            if (!dto.IsValid()) throw new InvalidEntityException(dto.Errors);

            await dto.FixDTO(_clienteRepository, _produtoRepository);

            List<ProdutoVenda> produtosVenda = dto.Produtos
                                                           .Select(produto => new ProdutoVenda(produtoId: produto.Id, quantidade: produto.Quantidade))
                                                           .ToList();

            foreach (ProdutoQtdCreateDTO produtoQtd in dto.Produtos)
            {
                long quantidade = produtoQtd.Quantidade;
                produtoQtd.Produto.ChangeStock(quantidade);
            }

            Venda venda = await _vendaRepository.SaveAsync(dto.ToModel(), produtosVenda);

            var viewDTO = new VendaViewDTO()
            {
                Venda = venda,
                Produtos = dto.Produtos
                                      .Select(p => p.ToViewDTO())
                                      .ToArray(),
            };

            return viewDTO;
        }


    }
}