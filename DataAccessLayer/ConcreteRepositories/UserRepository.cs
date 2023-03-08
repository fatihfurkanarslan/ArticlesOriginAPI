using DataAccessLayer.AbstractRepositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BlogContext context) : base(context)
        {

        }

        //public Task<List<User>> SearchPeopleAsync(Dictionary<string, string> ids)
        //{
        //    throw new NotImplementedException();
        //}
         
        public async Task<List<User>> SearchPeopleAsync(List<KeyValuePair<string, string>> ids)
        {
            List<User> users = new List<User>();
            List<string> followeeids = new List<string>();
            string followerid = null;
            List<string> lastIds = new List<string>();

            foreach (KeyValuePair<string, string> id in ids)
            {
                if(id.Key == "followeeId") followeeids.Add(id.Value);
                if(id.Key == "followerId") followerid = id.Value;
            }
 
            var result = await blogContext.Users.Join(blogContext.Followers, a => a.Id, b => b.FollowerId, (a, b) => new { a, b })
                .Where(b => followeeids.Contains(b.b.FolloweeId)).ToListAsync();

            foreach (var user in result)
            {
                lastIds.Add(user.b.FolloweeId);
            }

            var result2 = blogContext.Users.Where(x => lastIds.Contains(x.Id)).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                users.Add(new User
                {
                    Id = result2[i].Id,
                    UserName = result2[i].UserName,
                    Photourl = result2[i].Photourl,
                    Description = result2[i].Description
                });
            }

            return users;
        }

        public async Task<User> UpdateUser(User user)
        {
            blogContext.SaveChanges();
            User returnUser = await dbSetObject.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            return returnUser;
        }
    }
}
