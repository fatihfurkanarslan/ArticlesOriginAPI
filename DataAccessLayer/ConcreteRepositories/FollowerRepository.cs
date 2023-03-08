using DataAccessLayer.AbstractRepositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ConcreteRepositories
{
    public class FollowerRepository : Repository<Follower>, IFollowerRepository
    {
        public FollowerRepository(BlogContext context) : base(context)
        {
        }


        public new async Task<int> Insert(Follower follower)
        {
           var result = dbSetObject
                .Where(x => x.FollowerId == follower.FollowerId)
                .Where(y => y.FolloweeId == follower.FolloweeId).FirstOrDefault();

            if (result == null)
            {
                dbSetObject.Add(follower);
                return await blogContext.SaveChangesAsync();
            }
            else { return 0; }
        }

        public new async Task<int> Remove(Follower follower)
        {
            Follower result = dbSetObject
                 .Where(x => x.FollowerId == follower.FollowerId)
                 .Where(y => y.FolloweeId == follower.FolloweeId).FirstOrDefault();

            if (result != null)
            {
                dbSetObject.Remove(result);
                return await blogContext.SaveChangesAsync();
            }
            else { return 0; }
        }




    }
}
