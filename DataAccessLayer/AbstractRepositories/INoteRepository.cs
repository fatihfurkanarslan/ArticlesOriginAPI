using Entities;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.AbstractRepositories
{
    public interface INoteRepository : IRepository<Note>
    {


        Task<PagedList<Note>> GetListAsyncForNote(NoteParams noteParams);

        Task<PagedList<Note>> IncludeAsyncForNote(NoteParams noteParams, Expression<Func<Note, object>> includeFilter);

        Task<PagedList<Note>> IncludeAsyncForNoteByDescending(NoteParams noteParams, Expression<Func<Note, object>> includeFilter, Expression<Func<Note, object>> descendingFilter);

        Task<List<Note>> FindPopularNotes(Expression<Func<Note, bool>> filter, params string[] includetables);

        Task<List<Note>> IncludeSingleAsync(string tagParam);
    }
}
