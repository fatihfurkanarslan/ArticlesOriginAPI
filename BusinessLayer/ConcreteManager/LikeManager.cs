using DataAccessLayer;
using DataAccessLayer.UnitOfWork;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class LikeManager
    {
        //IRepository<Like> likeRepository;
        IUnitOfWork unitOfWork;
        public LikeManager(IUnitOfWork _unitOfWork)
        {
            //likeRepository = _likeRepository;
            unitOfWork = _unitOfWork;
        }

        public List<int> GetLikes(int userId, int[] likeIds)
        {

            List<int> likes = unitOfWork.Like.FindList(x => x.User.Id == userId && likeIds.Contains(x.Note.Id)).Select(x => x.Note.Id).ToList();

            return likes;
        }
      

        public async Task<int> Insert(Like like)
        {
            return await unitOfWork.Like.Insert(like);
        }

        public async Task<int> Delete(Like like)
        {
            return await unitOfWork.Like.Remove(like);
        }
    }
}
