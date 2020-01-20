using Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Repository<T> : IRepository<T>
        where T : class
 
    {
        private BlogContext blogContext;
        private DbSet<T> dbSetObject;

        public Repository(BlogContext context)
        {
            blogContext = context;
            dbSetObject = blogContext.Set<T>();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await dbSetObject.FirstOrDefaultAsync(filter);
        }

        public async Task<T> GetIncludeAsync(Expression<Func<T, bool>> filter, params string[] includetables)
        {
            IQueryable<T> query = filter == null ? dbSetObject : dbSetObject.Where(filter);

            includetables.ToList().ForEach(tableName =>
            {
                query = query.Include(tableName);
            });

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetListAsync()
        {
            return await dbSetObject.ToListAsync();
        }

        public async Task<PagedList<T>> GetListAsyncForNote(NoteParams noteParams)
        {
            var list = await dbSetObject.ToListAsync();

            var queryableList = list.AsQueryable();

            PagedList<T> returnValues = PagedList<T>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);

            //return await PagedList<T>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);
            return returnValues;
        }

        public async Task<PagedList<T>> IncludeAsyncForNote(NoteParams noteParams, Expression<Func<T, object>> includeFilter)
        {
            var list = await dbSetObject.Include(includeFilter).ToListAsync();

            var queryableList = list.AsQueryable();

            PagedList<T> returnValues = PagedList<T>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);

            //return await PagedList<T>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);
            return returnValues;
        }

        public async Task<PagedList<T>> IncludeAsyncForNoteByDescending(NoteParams noteParams, Expression<Func<T, object>> includeFilter, Expression<Func<T, object>> descendingFilter)
        {
            var list = await dbSetObject.Include(includeFilter).OrderByDescending(descendingFilter).ToListAsync();

            var queryableList = list.AsQueryable();

            PagedList<T> returnValues = PagedList<T>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);

            //return await PagedList<T>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);
            return returnValues;
        }

        public async Task<List<T>> IncludeAsync(Expression<Func<T, object>> includeFilter)
        {
            //var list = await dbSetObject.Include(includeFilter).ToListAsync();

            //var queryableList = list.AsQueryable();

            //PagedList<T> returnValues = PagedList<T>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);

            ////return await PagedList<T>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);
            //return returnValues;
             return await dbSetObject.Include(includeFilter).ToListAsync();
        }
        //for join tables
        public List<T> FindList(Expression<Func<T, bool>> filter)
        {
            return dbSetObject.Where(filter).ToList();
        }

        public async Task<int> Insert(T entity)
        {
             dbSetObject.Add(entity);
             return await blogContext.SaveChangesAsync();
        }

        public async Task<int> Remove(T entity)
        {
            dbSetObject.Remove(entity);
            return await blogContext.SaveChangesAsync();
        }

        public async Task<int> Update(T entity)
        {
            return await blogContext.SaveChangesAsync();
        }

        public async Task<List<T>> FindList(Expression<Func<T, bool>> filter, params string[] includetables)
        {
            IQueryable<T> query = filter==null?dbSetObject:dbSetObject.Where(filter);

            includetables.ToList().ForEach(tableName =>
            {
                query = query.Include(tableName);
            });

            return await query.ToListAsync();
        }

        public async Task<List<T>> FindPopularNotes(Expression<Func<T, bool>> filter, params string[] includetables)
        {
            IQueryable<T> query = filter == null ? dbSetObject : dbSetObject.Where(filter);

            includetables.ToList().ForEach(tableName =>
            {
                query = query.Include(tableName);
            });

            return await query.Take(5).ToListAsync();
        }
    }
}
