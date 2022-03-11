using Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AbstractManager
{
    public interface INoteManager
    {
        Task<List<Note>> FindPopularNotes(Expression<Func<Note, bool>> filter, params string[] includetables); 

    }
}
