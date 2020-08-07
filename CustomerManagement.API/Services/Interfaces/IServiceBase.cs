using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CustomerManagement.Entities;

namespace CustomerManagement.API.Services.Interfaces
{
    public interface IServiceBase<TEntity> where TEntity:class, IEntityBase
    {

        
        List<TEntity> Read(int page = 1 , int recordsPerPage = 100);

        List<TEntity> Read(int page, int recordsPerPage,
            IQueryable<TEntity> query,
            string orderBy = "", DataSortType dataSortType = DataSortType.Asc);

        TEntity Read(long id);

        List<TEntity> Read(
            Expression<Func<TEntity, bool>> filterExpression);

        IQueryable<TEntity> GetQueryable();

        TEntity Create(TEntity item);

        bool Exists(long id);
        
        long Count();

        TEntity Update(TEntity item);

        TEntity Delete(long id);

    }
    
    public enum DataSortType
    {
        Asc = 1,
        Desc = 2
    }
}