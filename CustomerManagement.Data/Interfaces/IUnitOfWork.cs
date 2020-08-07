using System;
using CustomerManagement.Entities;

namespace CustomerManagement.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        void Save();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntityBase;
    }
}