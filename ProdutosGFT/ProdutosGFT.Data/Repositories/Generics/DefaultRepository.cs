using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProdutosGFT.Domain.Interfaces.Generics.Entities;
using ProdutosGFT.Domain.Interfaces.Generics.Repositories;
using ProdutosGFT.Domain.Util.Exceptions;

namespace ProdutosGFT.Data.Repositories.Generics
{
    public class DefaultRepository<T> : IDefaultRepository<T> where T : class, IDefaultEntity
    {
        protected readonly ProdutosGFTDbContext _context;
        public DefaultRepository(ProdutosGFTDbContext context)
        {
            this._context = context;
        }

        public virtual async Task DeleteByIdAsync(long id)
        {
            var entity = await SelectByIdAsync(id);

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<T>> SelectAllAsync(Func<T, Object> orderByFilter, bool asc = true)
        {
            List<T> entities = await _context.Set<T>()
                                                    .AsNoTracking()
                                                    .ToListAsync();

            if (asc) entities = entities.OrderBy(orderByFilter).ToList();
            else entities = entities.OrderByDescending(orderByFilter).ToList();

            return entities;
        }



        public async Task<List<T>> SelectByConditionAsync(Func<T, bool> condition, Func<T, Object> orderByFilter, bool asc = true)
        {
            var result = _context.Set<T>()
                                      .AsNoTracking()
                                      .Where(condition);

            List<T> entities = await Task.FromResult(result.ToList());

            if (asc) entities = entities.OrderBy(orderByFilter).ToList();
            else entities = entities.OrderByDescending(orderByFilter).ToList();

            return entities;
        }

        public virtual async Task<T> SelectByIdAsync(long id, bool onlyAtivos = false)
        {
            if (id <= 0) throw new InvalidEntityException(msg: $"O Id '{id}' da entidade '{typeof(T).Name}' está inválido!", field: $"{typeof(T).Name}Id");

            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && (onlyAtivos ? e.IsAtivo : true));

            if (entity == null) throw new EntityNotFoundException($"A entidade '{typeof(T).Name}' com Id '{id}' não existe ou está inativa!");

            return entity;
        }

        public virtual async Task<T> SelectByIdAsNoTrackingAsync(long id, bool onlyAtivos = false)
        {
            if (id <= 0) throw new InvalidEntityException(msg: $"O Id '{id}' da entidade '{typeof(T).Name}' está inválido!", field: $"{typeof(T).Name}Id");

            var entity = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id && (onlyAtivos ? e.IsAtivo : true));

            if (entity == null) throw new EntityNotFoundException($"A entidade '{typeof(T).Name}' com Id '{id}' não existe ou está inativa!");

            return entity;
        }



        public virtual async Task<T> SaveAsync(T entity)
        {
            if (entity == null) throw new InvalidEntityException(msg: $"A entidade '{typeof(T).Name}' está inválida!", field: typeof(T).Name);
            if (entity.Id < 0) throw new InvalidEntityException(msg: $"O Id '{entity.Id}' da entidade '{typeof(T).Name}' está inválido!", field: $"{typeof(T).Name}Id");

            EntityState state = EntityState.Modified;
            if (entity.Id == 0)
            {
                state = EntityState.Added;
            }
            entity.DataAlteracao = DateTime.Now;

            _context.Entry(entity).State = state;
            await _context.SaveChangesAsync();

            return entity;
        }


    }
}