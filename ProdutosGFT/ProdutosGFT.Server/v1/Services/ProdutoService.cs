using System;
using System.Collections.Generic;
using ProdutosGFT.Server.v1.DTOs.ProdutoDTOs;
using ProdutosGFT.Domain.Interfaces.Repositories;
using ProdutosGFT.Server.v1.Interfaces.Services;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Domain.Util.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using ProdutosGFT.Domain.Util.Pagination;

namespace ProdutosGFT.Server.v1.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        public ProdutoService(IProdutoRepository produtoRepository, IFornecedorRepository fornecedorRepository)
        {
            this._produtoRepository = produtoRepository;
            this._fornecedorRepository = fornecedorRepository;
        }

        #region Gets

        #region GetAll

        public virtual async Task<PagedList<ProdutoViewDTO>> GetAllAsync(PaginationParameters pagination)
        {
            IEnumerable<ProdutoViewDTO> produtos = (await _produtoRepository
                                                                            .SelectByConditionAsync(
                                                                                orderByFilter: produto => produto.Nome,
                                                                                condition: fornecedor => fornecedor.IsAtivo
                                                                            )).Select(produto => new ProdutoViewDTO()
                                                                            {
                                                                                Produto = produto
                                                                            });

            return PagedList<ProdutoViewDTO>.ToPagedList(produtos, pagination.pageNumber, pagination.pageSize);
        }

        public virtual async Task<PagedList<ProdutoViewDTO>> GetAllAscToNameAsync(PaginationParameters pagination)
        {
            IEnumerable<ProdutoViewDTO> produtos = (await _produtoRepository
                                                                            .SelectByConditionAsync(
                                                                                orderByFilter: produto => produto.Nome,
                                                                                condition: fornecedor => fornecedor.IsAtivo,
                                                                                asc: true
                                                                            )).Select(produto => new ProdutoViewDTO()
                                                                            {
                                                                                Produto = produto
                                                                            });

            return PagedList<ProdutoViewDTO>.ToPagedList(produtos, pagination.pageNumber, pagination.pageSize);
        }

        public virtual async Task<PagedList<ProdutoViewDTO>> GetAllDescToNameAsync(PaginationParameters pagination)
        {
            IEnumerable<ProdutoViewDTO> produtos = (await _produtoRepository
                                                                            .SelectByConditionAsync(
                                                                                orderByFilter: produto => produto.Nome,
                                                                                condition: fornecedor => fornecedor.IsAtivo,
                                                                                asc: false
                                                                            )).Select(produto => new ProdutoViewDTO()
                                                                            {
                                                                                Produto = produto
                                                                            });

            return PagedList<ProdutoViewDTO>.ToPagedList(produtos, pagination.pageNumber, pagination.pageSize);
        }

        public virtual async Task<PagedList<ProdutoViewDTO>> GetByNomeAsync(string nome, PaginationParameters pagination)
        {
            if (string.IsNullOrEmpty(nome)) throw new InvalidEntityException(msg: $"O Nome da entidade 'Produto' está inválido!", field: $"Nome");

            IEnumerable<ProdutoViewDTO> produtos = (await _produtoRepository
                                                                            .SelectByConditionAsync(
                                                                                orderByFilter: produto => produto.Nome,
                                                                                condition: produto =>
                                                                                 produto.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase)
                                                                            )).Select(produto => new ProdutoViewDTO()
                                                                            {
                                                                                Produto = produto
                                                                            });

            return PagedList<ProdutoViewDTO>.ToPagedList(produtos, pagination.pageNumber, pagination.pageSize);
        }

        #endregion

        public virtual async Task<ProdutoViewDTO> GetByIdAsync(long id)
        {
            var produtoViewDTO = new ProdutoViewDTO()
            {
                Produto = await _produtoRepository.SelectByIdAsNoTrackingAsync(id, onlyAtivos: true)
            };
            return produtoViewDTO;
        }

        #endregion

        public virtual async Task<ProdutoViewDTO> InsertAsync(ProdutoCreateDTO dto)
        {
            if (!dto.IsValid()) throw new InvalidEntityException(dto.Errors);

            await _fornecedorRepository.SelectByIdAsNoTrackingAsync(dto.FornecedorId.Value, onlyAtivos: true);

            var produtoViewDTO = new ProdutoViewDTO()
            {
                Produto = await _produtoRepository.SaveAsync(dto.ToModel())
            };
            return produtoViewDTO;
        }

        public virtual async Task<ProdutoViewDTO> PartialUpdateAsync(ProdutoUpdateDTO dto)
        {
            if (!dto.IsValid(isPatch: true)) throw new InvalidEntityException(dto.Errors);

            Produto produto = await _produtoRepository.SelectByIdAsync(dto.Id, onlyAtivos: true);

            if (dto.FornecedorId.HasValue)
            {
                await _fornecedorRepository.SelectByIdAsNoTrackingAsync(dto.FornecedorId.Value);

                produto.FornecedorId = dto.FornecedorId.Value;
            }

            produto.Imagem = dto.Imagem ?? produto.Imagem;
            produto.Nome = dto.Nome ?? produto.Nome;
            produto.Promocao = dto.Promocao ?? produto.Promocao;
            produto.Quantidade = dto.Quantidade ?? produto.Quantidade;
            produto.Valor = dto.Valor ?? produto.Valor;
            produto.ValorPromo = dto.ValorPromo ?? produto.ValorPromo;

            var produtoViewDTO = new ProdutoViewDTO()
            {
                Produto = await _produtoRepository.SaveAsync(produto)
            };
            return produtoViewDTO;
        }

        public virtual async Task<ProdutoViewDTO> UpdateAsync(ProdutoUpdateDTO dto)
        {
            if (!dto.IsValid()) throw new InvalidEntityException(dto.Errors);

            await _fornecedorRepository.SelectByIdAsNoTrackingAsync(dto.FornecedorId.Value, onlyAtivos: true);
            await _produtoRepository.SelectByIdAsNoTrackingAsync(dto.Id, onlyAtivos: true);

            var produtoViewDTO = new ProdutoViewDTO()
            {
                Produto = await _produtoRepository.SaveAsync(dto.ToModel())
            };
            return produtoViewDTO;
        }

        public virtual async Task DeleteByIdAsync(long id)
        {
            await _produtoRepository.DeleteByIdAsync(id);
        }

        public virtual async Task DesativeByIdAsync(long id)
        {
            Produto produto = await _produtoRepository.SelectByIdAsync(id);
            produto.Desactivate();

            await _produtoRepository.SaveAsync(produto);
        }

        public virtual async Task ActiveByIdAsync(long id)
        {
            Produto produto = await _produtoRepository.SelectByIdAsync(id);
            produto.Activate();

            await _produtoRepository.SaveAsync(produto);
        }
    }
}