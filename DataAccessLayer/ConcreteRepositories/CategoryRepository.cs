using DataAccessLayer.AbstractRepositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BlogContext context) : base(context)
        {

        }
    }
}
