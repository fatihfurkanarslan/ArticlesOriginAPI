using DataAccessLayer.AbstractRepositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(BlogContext context) :base(context)
        {

        }
    }
}
