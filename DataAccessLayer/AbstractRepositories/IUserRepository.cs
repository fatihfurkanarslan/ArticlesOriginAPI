using Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.AbstractRepositories
{
    public interface IUserRepository : IRepository<User>
    {
         Task<List<User>> SearchPeopleAsync(List<KeyValuePair<string, string>> ids);

        Task<User> UpdateUser(User user);

    }


}
