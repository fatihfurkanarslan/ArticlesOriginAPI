using DataAccessLayer;
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
        IRepository<Like> likeRepository;
        public LikeManager(IRepository<Like> _likeRepository)
        {
            likeRepository = _likeRepository;
        }

        public List<int> GetLikes(int userId, int[] likeIds)
        {

            List<int> likes = likeRepository.FindList(x => x.User.Id == userId && likeIds.Contains(x.Note.Id)).Select(x => x.Note.Id).ToList();

            return likes;
        }
      

        public async Task<int> Insert(Like like)
        {
            return await likeRepository.Insert(like);
        }

        public async Task<int> Delete(Like like)
        {
            return await likeRepository.Remove(like);
        }
    }
}
