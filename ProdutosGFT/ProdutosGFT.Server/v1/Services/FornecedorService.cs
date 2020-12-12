using System;
using System.Collections.Generic;
using ProdutosGFT.Server.v1.DTOs.FornecedorDTOs;
using ProdutosGFT.Domain.Interfaces.Repositories;
using ProdutosGFT.Server.v1.Interfaces.Services;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Domain.Util.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using ProdutosGFT.Domain.Util.Pagination;

namespace ProdutosGFT.Server.v1.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IProdutoRepository _produtoRepository;
        public FornecedorService(IFornecedorRepository fornecedorRepository, IProdutoRepository produtoRepository)
        {
            this._produtoRepository = produtoRepository;
            this._fornecedorRepository = fornecedorRepository;
        }

        #region Gets

        #region GetAll

        public virtual async Task<PagedList<FornecedorViewDTO>> GetAllAsync(PaginationParameters pagination)
        {
            IEnumerable<FornecedorViewDTO> fornecedores = (await _fornecedorRepository
                                                                                    .SelectByConditionAsync(
                                                                                        orderByFilter: fornecedor => fornecedor.Nome,
                                                                                        condition: fornecedor => fornecedor.IsAtivo
                                                                                    )).Select(fornecedor => new FornecedorViewDTO()
                                                                                    {
                                                                                        Fornecedor = fornecedor
                                                                                    });

            return PagedList<FornecedorViewDTO>.ToPagedList(fornecedores, pagination.pageNumber, pagination.pageSize);
        }

        public virtual async Task<PagedList<FornecedorViewDTO>> GetAllAscToNameAsync(PaginationParameters pagination)
        {
            IEnumerable<FornecedorViewDTO> fornecedores = (await _fornecedorRepository
                                                                                   .SelectByConditionAsync(
                                                                                       orderByFilter: fornecedor => fornecedor.Nome,
                                                                                       condition: fornecedor => fornecedor.IsAtivo,
                                                                                       asc: true
                                                                                   )).Select(fornecedor => new FornecedorViewDTO()
                                                                                   {
                                                                                       Fornecedor = fornecedor
                                                                                   });

            return PagedList<FornecedorViewDTO>.ToPagedList(fornecedores, pagination.pageNumber, pagination.pageSize);
        }

        public virtual async Task<PagedList<FornecedorViewDTO>> GetAllDescToNameAsync(PaginationParameters pagination)
        {
            IEnumerable<FornecedorViewDTO> fornecedores = (await _fornecedorRepository
                                                                                   .SelectByConditionAsync(
                                                                                       orderByFilter: fornecedor => fornecedor.Nome,
                                                                                       condition: fornecedor => fornecedor.IsAtivo,
                                                                                       asc: false
                                                                                   )).Select(fornecedor => new FornecedorViewDTO()
                                                                                   {
                                                                                       Fornecedor = fornecedor
                                                                                   });

            return PagedList<FornecedorViewDTO>.ToPagedList(fornecedores, pagination.pageNumber, pagination.pageSize);
        }

        public virtual async Task<PagedList<FornecedorViewDTO>> GetByNomeAsync(string nome, PaginationParameters pagination)
        {
            if (string.IsNullOrEmpty(nome)) throw new InvalidEntityException(msg: $"O Nome da entidade 'FornecedorViewDTO' está inválido!", field: $"Nome");

            IEnumerable<FornecedorViewDTO> fornecedores = (await _fornecedorRepository
                                                                                .SelectByConditionAsync(
                                                                                    orderByFilter: fornecedor => fornecedor.Nome,
                                                                                    condition: fornecedor =>
                                                                                    fornecedor.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase)
                                                                                )).Select(fornecedor => new FornecedorViewDTO()
                                                                                {
                                                                                    Fornecedor = fornecedor
                                                                                });

            return PagedList<FornecedorViewDTO>.ToPagedList(fornecedores, pagination.pageNumber, pagination.pageSize);
        }

        #endregion

        public virtual async Task<FornecedorViewDTO> GetByIdAsync(long id)
        {
            var fornecedorViewDTO = new FornecedorViewDTO()
            {
                Fornecedor = await _fornecedorRepository.SelectByIdAsNoTrackingAsync(id, onlyAtivos: true)
            };
            return fornecedorViewDTO;
        }

        public virtual async Task<FornecedorViewDTO> GetByIdWithProdutosAsync(long id)
        {
            var fornecedorViewDTO = new FornecedorViewDTO()
            {
                Fornecedor = await _fornecedorRepository.SelectByIdWithProdutosAsync(id)
            };
            return fornecedorViewDTO;
        }



        #endregion

        public virtual async Task<FornecedorViewDTO> InsertAsync(FornecedorCreateDTO dto)
        {
            if (!dto.IsValid()) throw new InvalidEntityException(dto.Errors);

            var fornecedorViewDTO = new FornecedorViewDTO()
            {
                Fornecedor = await _fornecedorRepository.SaveAsync(dto.ToModel())
            };
            return fornecedorViewDTO;
        }

        public virtual async Task<FornecedorViewDTO> PartialUpdateAsync(FornecedorUpdateDTO dto)
        {
            if (!dto.IsValid(isPatch: true)) throw new InvalidEntityException(dto.Errors);

            var fornecedor = await _fornecedorRepository.SelectByIdAsync(dto.Id, onlyAtivos: true);

            fornecedor.Cnpj = dto.Cnpj ?? fornecedor.Cnpj;
            fornecedor.Nome = dto.Nome ?? fornecedor.Nome;

            var fornecedorViewDTO = new FornecedorViewDTO()
            {
                Fornecedor = await _fornecedorRepository.SaveAsync(fornecedor)
            };
            return fornecedorViewDTO;
        }

        public virtual async Task<FornecedorViewDTO> UpdateAsync(FornecedorUpdateDTO dto)
        {
            if (!dto.IsValid()) throw new InvalidEntityException(dto.Errors);

            await _fornecedorRepository.SelectByIdAsNoTrackingAsync(dto.Id, onlyAtivos: true);

            var fornecedorViewDTO = new FornecedorViewDTO()
            {
                Fornecedor = await _fornecedorRepository.SaveAsync(dto.ToModel())
            };
            return fornecedorViewDTO;
        }

        public virtual async Task DeleteByIdAsync(long id)
        {
            await _fornecedorRepository.DeleteByIdAsync(id);
        }

        public virtual async Task DesativeByIdAsync(long id)
        {
            Fornecedor fornecedor = await _fornecedorRepository.SelectByIdAsync(id);
            fornecedor.Desactivate();

            await _fornecedorRepository.SaveAsync(fornecedor);
        }

        public virtual async Task ActiveByIdAsync(long id)
        {
            Fornecedor fornecedor = await _fornecedorRepository.SelectByIdAsync(id);
            fornecedor.Activate();

            await _fornecedorRepository.SaveAsync(fornecedor);
        }



    }
}