using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Itopya.Domain.Entities.Abstract;
using Itopya.Domain.Entities.RequestFeatures;
using Itopya.Domain.Repositories;
using Itopya.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Itopya.Infrastructure.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<T> table;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task<PagedList<T>> GetFilteredList(RequestParameters requestParameters, Expression<Func<T, T>> selector = null,
            Func<IQueryable<T>, IQueryable<T>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true)
        {
            var query = Filter(selector, predicate, include, disableTracking);

            return await PagedList<T>.ToPagedList(query, requestParameters.PageNumber, requestParameters.PageSize);
        }

        public async Task<List<T>> GetList(Func<IQueryable<T>, IQueryable<T>> predicate = null, Expression<Func<T, T>> selector = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disableTracking = true)
        {
            var query = Filter(selector, predicate, include, disableTracking);
            return await query.ToListAsync();

        }
        public async Task Add(T entity)
        {
            await _context.AddAsync(entity);
        }
        public void Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            table.Remove(entity);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> selector = null)
        {
            if (selector == null)
            {
                return await table.Where(predicate).FirstOrDefaultAsync();
            }
            return await table.Where(predicate).Select(selector).FirstOrDefaultAsync();
        }

        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return await table.AnyAsync(predicate);
        }

        public async Task<T> GetById(int id)
        {
            return await table.FindAsync(id);
        }

        public async Task<List<T>> FromSqlRaw(string sql, params object[] parameters)
        {
            return await table.FromSqlRaw(sql, parameters).ToListAsync();
        }
        public async Task<int> ExecuteSqlRaw(string sql, params object[] parameters)
        {
           return await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }
        private IQueryable<T> Filter(Expression<Func<T, T>> selector = null,
            Func<IQueryable<T>, IQueryable<T>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = table;

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = predicate(query);
            }

            if (selector != null)
            {
                query = query.Select(selector);
            }
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
    }
}