using BusinessLayer.AbstractManager;
using DataAccessLayer;
using DataAccessLayer.UnitOfWork;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CommentManager : ICommentManager
    {
        //IRepository<Comment> commentRepository;
        IUnitOfWork unitOfWork;
        public CommentManager(IUnitOfWork _unitOfWork)
        {
            //commentRepository = _commentRepository;
            unitOfWork = _unitOfWork;
        }

        public async Task<List<Comment>> GetComments()
        {
            return await unitOfWork.Comment.GetListAsync();
           
        }
        public async Task<Comment> GetComment(int id)
        {
            var returnValue = await unitOfWork.Comment.GetAsync(x => x.Id == id);
            return returnValue;
        }

        public async Task<int> Insert(Comment comment)
        {
            return await unitOfWork.Comment.Insert(comment);
        }

        public async Task<int> Update(Comment comment)
        {
            return await unitOfWork.Comment.Update(comment);
        }

        public async Task<int> Delete(Comment comment)
        {
            return await unitOfWork.Comment.Remove(comment);
        }
    }
}
