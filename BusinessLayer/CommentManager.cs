using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CommentManager
    {
        IRepository<Comment> commentRepository;
        public CommentManager(IRepository<Comment> _commentRepository)
        {
            commentRepository = _commentRepository;
        }

        public async Task<List<Comment>> GetComments()
        {
            var returnValues = await commentRepository.GetListAsync();
            return returnValues;
        }
        public async Task<Comment> GetComment(int id)
        {
            var returnValue = await commentRepository.GetAsync(x => x.Id == id);
            return returnValue;
        }

        public async Task<int> Insert(Comment comment)
        {
            return await commentRepository.Insert(comment);
        }

        public async Task<int> Update(Comment comment)
        {
            return await commentRepository.Update(comment);
        }

        public async Task<int> Delete(Comment comment)
        {
            return await commentRepository.Remove(comment);
        }
    }
}
