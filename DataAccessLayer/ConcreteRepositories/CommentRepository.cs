using DataAccessLayer.AbstractRepositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext contex) : base(contex)
        {

        }
    }
}
