using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Server.v1.DTOs.ClienteDTOs;
using ProdutosGFT.Domain.Interfaces.Repositories;
using ProdutosGFT.Server.v1.Interfaces.Services;
using System.Collections.Generic;
using System;
using ProdutosGFT.Domain.Util.Exceptions;
using System.Linq;
using ProdutosGFT.Domain.Util.Helpers;
using System.Threading.Tasks;
using ProdutosGFT.Domain.Util.Pagination;

namespace ProdutosGFT.Server.v1.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        public ClienteService(IClienteRepository repository)
        {
            this._repository = repository;
        }

        #region Gets

        #region GetAll

        public virtual async Task<PagedList<ClienteViewDTO>> GetAllAsync(PaginationParameters pagination)
        {
            IEnumerable<ClienteViewDTO> clientes = (await _repository
                                                                    .SelectByConditionAsync(
                                                                        orderByFilter: cliente => cliente.Id,
                                                                        condition: cliente => cliente.IsAtivo
                                                                    )).Select(cliente => new ClienteViewDTO()
                                                                    {
                                                                        Cliente = cliente
                                                                    });

            return PagedList<ClienteViewDTO>.ToPagedList(clientes, pagination.pageNumber, pagination.pageSize);

        }

        public virtual async Task<PagedList<ClienteViewDTO>> GetAllAscToNameAsync(PaginationParameters pagination)
        {
            IEnumerable<ClienteViewDTO> clientes = (await _repository
                                                                    .SelectByConditionAsync(
                                                                        orderByFilter: cliente => cliente.Nome,
                                                                        condition: cliente => cliente.IsAtivo,
                                                                        asc: true
                                                                    )).Select(cliente => new ClienteViewDTO()
                                                                    {
                                                                        Cliente = cliente
                                                                    });

            return PagedList<ClienteViewDTO>.ToPagedList(clientes, pagination.pageNumber, pagination.pageSize);
        }

        public virtual async Task<PagedList<ClienteViewDTO>> GetAllDescToNameAsync(PaginationParameters pagination)
        {
            IEnumerable<ClienteViewDTO> clientes = (await _repository
                                                                    .SelectByConditionAsync(
                                                                        orderByFilter: cliente => cliente.Nome,
                                                                        condition: cliente => cliente.IsAtivo,
                                                                        asc: false
                                                                    )).Select(cliente => new ClienteViewDTO()
                                                                    {
                                                                        Cliente = cliente
                                                                    });

            return PagedList<ClienteViewDTO>.ToPagedList(clientes, pagination.pageNumber, pagination.pageSize);
        }

        public virtual async Task<PagedList<ClienteViewDTO>> GetByNomeAsync(string nome, PaginationParameters pagination)
        {
            if (string.IsNullOrEmpty(nome)) throw new InvalidEntityException(msg: $"O Nome da entidade 'Cliente' está inválido!", field: $"ClienteId");

            IEnumerable<ClienteViewDTO> clientes = (await _repository
                                                                   .SelectByConditionAsync(
                                                                   orderByFilter: cliente => cliente.Nome,
                                                                   condition: cliente => cliente.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase)
                                                                   )).Select(cliente => new ClienteViewDTO()
                                                                   {
                                                                       Cliente = cliente
                                                                   });

            return PagedList<ClienteViewDTO>.ToPagedList(clientes, pagination.pageNumber, pagination.pageSize);
        }

        #endregion

        public virtual async Task<ClienteViewDTO> GetByIdAsync(long id)
        {
            var clienteViewDTO = new ClienteViewDTO()
            {
                Cliente = await _repository.SelectByIdAsNoTrackingAsync(id, onlyAtivos: true)
            };

            return clienteViewDTO;
        }



        #endregion

        public virtual async Task<ClienteViewDTO> UpdateAsync(ClienteUpdateDTO dto)
        {
            if (!dto.IsValid()) throw new InvalidEntityException(dto.Errors);

            await _repository.SelectByIdAsNoTrackingAsync(dto.Id, onlyAtivos: true);

            var clienteViewDTO = new ClienteViewDTO()
            {
                Cliente = await _repository.SaveAsync(dto.ToModel())
            };

            return clienteViewDTO;
        }

        public virtual async Task<ClienteViewDTO> PartialUpdateAsync(ClienteUpdateDTO dto)
        {
            if (!dto.IsValid(isPatch: true)) throw new InvalidEntityException(dto.Errors);

            Cliente cliente = await _repository.SelectByIdAsync(dto.Id, onlyAtivos: true);

            cliente.Documento = dto.Documento ?? cliente.Documento;
            cliente.Email = dto.Email ?? cliente.Email;
            cliente.Nome = dto.Nome ?? cliente.Nome;
            cliente.Role = dto.Role ?? cliente.Role;
            if (!string.IsNullOrEmpty(dto.Senha))
            {
                cliente.Senha = MD5Hash.CreateHash(dto.Senha);
            }

            var clienteViewDTO = new ClienteViewDTO()
            {
                Cliente = await _repository.SaveAsync(cliente)
            };

            return clienteViewDTO;
        }

        public virtual async Task<ClienteViewDTO> InsertAsync(ClienteCreateDTO dto)
        {
            if (!dto.IsValid()) throw new InvalidEntityException(dto.Errors);

            var clienteViewDTO = new ClienteViewDTO()
            {
                Cliente = await _repository.SaveAsync(dto.ToModel())
            };

            return clienteViewDTO;
        }

        public virtual async Task DeleteByIdAsync(long id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public virtual async Task DesativeByIdAsync(long id)
        {
            Cliente cliente = await _repository.SelectByIdAsync(id);
            cliente.Desactivate();

            await _repository.SaveAsync(cliente);
        }

        public virtual async Task ActiveByIdAsync(long id)
        {
            Cliente cliente = await _repository.SelectByIdAsync(id);
            cliente.Activate();

            await _repository.SaveAsync(cliente);
        }


        #region Login

        public virtual async Task<TokenDTO> LoginAsync(LoginDTO loginDTO)
        {
            if (!loginDTO.IsValid()) throw new InvalidEntityException(loginDTO.Errors);

            Cliente cliente = (await _repository.SelectByConditionAsync(
                                            condition: cliente => EqualsCredentials(loginDTO, cliente) && cliente.IsAtivo,
                                            orderByFilter: cliente => cliente.Id
                                        ))
                                        .FirstOrDefault();

            if (cliente == null) throw new EntityNotFoundException(msg: $"Usuário não encontrado!");

            var tokenDTO = new TokenDTO()
            {
                Email = cliente.Email,
                Nome = cliente.Nome,
                Role = cliente.Role,
                Token = TokenService.GenerateToken(cliente)
            };

            return tokenDTO;
        }

        private bool EqualsCredentials(LoginDTO dto, Cliente cliente)
        {
            bool sameEmail = cliente.Email.Equals(dto.Email.Trim(), StringComparison.InvariantCultureIgnoreCase);
            bool sameSenha = cliente.Senha == MD5Hash.CreateHash(dto.Senha);

            return sameEmail && sameSenha;
        }

        #endregion
    }
}