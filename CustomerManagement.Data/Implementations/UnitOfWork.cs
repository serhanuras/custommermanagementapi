using System.Collections.Generic;
using CustomerManagement.Data.Interfaces;
using CustomerManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Data.Implementations
{
    public class UnitOfWork<T>:IUnitOfWork where T:DbContext
    {
        private readonly Dictionary<string,object> _repositories;
        private readonly T _context;
        
        public UnitOfWork(T context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntityBase
        {
            var repositoryName = typeof(TEntity).Name;

            var repository = new Repository<TEntity>(_context);
            if (!_repositories.ContainsKey(repositoryName))
                _repositories.Add(repositoryName, repository);

            return (Repository<TEntity>) _repositories[repositoryName];
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}