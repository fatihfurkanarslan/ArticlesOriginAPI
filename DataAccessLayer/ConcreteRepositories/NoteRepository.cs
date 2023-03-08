using DataAccessLayer.AbstractRepositories;
using Entities;
using Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        public NoteRepository(BlogContext context) : base(context)
        {

        }


        public new List<Note> FindList(Expression<Func<Note, bool>> filter)
        {
            return dbSetObject.Where(filter).Where(x => x.Deleted == false).ToList();
        }

        public new async Task<List<Note>> FindListAsync(Expression<Func<Note, bool>> filter)
        {
            return await dbSetObject.Where(filter).Where(x => x.Deleted == false).ToListAsync();
        }


        public new async Task<List<Note>> FindList(Expression<Func<Note, bool>> filter, params string[] includetables)
        {
            IQueryable<Note> query = filter == null ? dbSetObject : dbSetObject.Where(filter).Where(x => x.Deleted == false);

            includetables.ToList().ForEach(tableName =>
            {
                query = query.Include(tableName);
            });

            return await query.ToListAsync();
        }

        public async Task<List<Note>> FindPopularNotes(Expression<Func<Note, bool>> filter, params string[] includetables)
        {
            IQueryable<Note> query = filter == null ? dbSetObject : dbSetObject.Where(filter).Where(x => x.Deleted == false);

            includetables.ToList().ForEach(tableName =>
            {
                query = query.Include(tableName);
            });

            return await query.Take(5).ToListAsync();
        }
        public async Task<PagedList<Note>> GetListAsyncForNote(NoteParams noteParams)
        {
            var list = await dbSetObject.Where(x => x.Deleted == false).ToListAsync();

            var queryableList = list.AsQueryable();

            PagedList<Note> returnValues = PagedList<Note>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);

            //return await PagedList<T>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);
            return returnValues;
        }

        public async Task<PagedList<Note>> IncludeAsyncForNote(NoteParams noteParams, Expression<Func<Note, object>> includeFilter)
        {
            var list = await dbSetObject.Include(includeFilter).Where(x => x.Deleted == false).ToListAsync();

            var queryableList = list.AsQueryable();

            PagedList<Note> returnValues = PagedList<Note>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);

            //return await PagedList<T>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);
            return returnValues;
        }

        public async Task<PagedList<Note>> IncludeAsyncForNoteByDescending(NoteParams noteParams, Expression<Func<Note, object>> includeFilter, Expression<Func<Note, object>> descendingFilter)
        {
            var list = await dbSetObject.Include(includeFilter).OrderByDescending(descendingFilter).Where(x => x.Deleted == false).ToListAsync();

            var queryableList = list.AsQueryable();

            PagedList<Note> returnValues = PagedList<Note>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);

            //return await PagedList<T>.CreateAsync(queryableList, noteParams.PageNumber, noteParams.PageSize);
            return returnValues;
        }

        public async Task<List<Note>> IncludeSingleAsync(string tagParam) {
            return await dbSetObject.Where(note => note.Tags.Any(tag => tag.Tags.Contains(tagParam))).Where(x => x.Deleted == false).ToListAsync();
        }
    }
}
