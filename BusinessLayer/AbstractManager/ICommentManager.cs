using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AbstractManager
{
    public interface ICommentManager
    {
        Task<Comment> GetComment(int id);
        Task<List<Comment>> GetComments();

        Task<int> Insert(Comment comment);
        Task<int> Update(Comment comment);
        Task<int> Delete(Comment comment);

    }
}
