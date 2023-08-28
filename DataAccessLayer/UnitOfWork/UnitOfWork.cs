using DataAccessLayer.AbstractRepositories;
using DataAccessLayer.ConcreteRepositories;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
        ,IDisposable   
    {
        private BlogContext context;

        private CategoryRepository _categoryRepository;
        private CommentRepository _commentRepository;
        private LikeRepository _likeRepository;
        private NoteRepository _noteRepository;
        private PhotoRepository _photoRepository;
        private TagRepository _tagRepository;
        private UserRepository _userRepository;
        private FollowerRepository _followerRepository;


        public UnitOfWork(BlogContext _context)
        {
            context = _context;

        }

        public ICategoryRepository Category => _categoryRepository = _categoryRepository ?? new CategoryRepository(context);

        public ICommentRepository Comment => _commentRepository = _commentRepository ?? new CommentRepository(context);

        public ILikeRepository Like => _likeRepository = _likeRepository ?? new LikeRepository(context);

        public INoteRepository Note => _noteRepository = _noteRepository ?? new NoteRepository(context);

        public IPhotoRepository Photo => _photoRepository = _photoRepository ?? new PhotoRepository(context);

        public ITagRepository Tag => _tagRepository = _tagRepository ?? new TagRepository(context);

        public IUserRepository User => _userRepository = _userRepository ?? new UserRepository(context);

        public IFollowerRepository Follower => _followerRepository = _followerRepository ?? new FollowerRepository(context);

 

        public Task<int> CommitAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
