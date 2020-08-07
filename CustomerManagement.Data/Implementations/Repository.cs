
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CustomerManagement.Data.Interfaces;
using CustomerManagement.Entities;

namespace CustomerManagement.Data.Implementations
{
     public class Repository<T> : IRepository<T> where T : class, IEntityBase
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            this._dbSet = context.Set<T>();
        }

        
        public  T Add(T entity)
        {
            this._dbSet.Add(entity);

            return entity;
        }

        public T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public T Get(long id, string includeProperties)
        {
            IQueryable<T> query = _dbSet;
            
            
            if(!String.IsNullOrWhiteSpace(includeProperties))
                query = AddIncludes(query, includeProperties);

            var result = query.Where(x => x.Id == id).AsNoTracking().ToList();

            if (result.Count == 0)
                return null;
            
            return result[0];
        }

        private IQueryable<T> AddIncludes(IQueryable<T> queryable, string includeProperties)
        {
            if(includeProperties != null)
            {
                foreach(var includeProperty in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            return queryable;
        }
        

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int page = 1,
            int recordPerPage = 100)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if(!String.IsNullOrWhiteSpace(includeProperties))
                query = AddIncludes(query, includeProperties);

            query = query.Paginate(page, recordPerPage);
            
            if (orderBy != null)
            {
                return orderBy(query).AsNoTracking().ToList();
            }
            
            return query.AsNoTracking().ToList();
        }
        
        public IEnumerable<T> GetAll(
            IQueryable<T> query,
            string includeProperties = null,
            int page = 1,
            int recordPerPage = 100)
        {

            query = AddIncludes(query, includeProperties);

            query = query.Paginate(page, recordPerPage);
            
            return query.AsNoTracking().ToList();
        }

        public IQueryable<T> Queryable()
        {
            IQueryable<T> query = _dbSet;

            return query;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            //include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return  query.FirstOrDefault();
        }

        public T Remove(long id)
        {
            T entityToRemove = _dbSet.Find(id);
            
            if (entityToRemove == null)
            {
                return null;
            }
            
            return Remove(entityToRemove);
        }

        public bool Exists(long id)
        {
            return _dbSet.Any(x => x.Id == id);
        }

        public T Remove(T entity)
        {
            _dbSet.Remove(entity);

            return entity;
        }

        public long Count()
        {
            return _dbSet.Count();
        }
    }
}