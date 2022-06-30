using Helper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{

    public interface IRepository<T> 
        where T : class
    {
        
        Task<T> GetAsync(Expression<Func<T, bool>> filter);

        Task<T> GetIncludeAsync(Expression<Func<T, bool>> filter, params string[] includetables);

        Task<List<T>> GetListAsync();
 
        List<T> FindList(Expression<Func<T, bool>> filter);

        Task<List<T>> IncludeAsync(Expression<Func<T, object>> includeFilter);

        Task<int> Insert(T entity);

        Task<int> Remove(T entity);

        Task<int> Update(T entity);

        Task<List<T>> FindList(Expression<Func<T, bool>> filter, params string[] includetables);

       
    }
}
