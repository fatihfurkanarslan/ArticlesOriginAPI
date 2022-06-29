﻿using DataAccessLayer.AbstractRepositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ICommentRepository Comment { get; }
        ILikeRepository Like { get; }
        INoteRepository Note { get; }
        IPhotoRepository Photo { get; }
        ITagRepository Tag { get; }
        IUserRepository User { get; }
        IFollowerRepository Follower { get; }

        Task<int> CommitAsync();
        void Dispose();
    }
}
