using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Itopya.Domain.Entities.Abstract;
using Itopya.Domain.Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore.Query;

namespace Itopya.Domain.Repositories
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        Task<T> GetById(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> selector = null);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<PagedList<T>> GetFilteredList(RequestParameters requestParameters, Expression<Func<T, T>> selector = null,
             Func<IQueryable<T>, IQueryable<T>> predicate = null,
             Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
             bool disableTracking = true);
        Task<List<T>> GetList(Func<IQueryable<T>, IQueryable<T>> predicate = null, Expression<Func<T, T>> selector = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);
        Task<bool> Any(Expression<Func<T, bool>> predicate);
        Task<List<T>> FromSqlRaw(string sql, params object[] parameters);
        Task<int> ExecuteSqlRaw(string sql, params object[] parameters);
    }
}