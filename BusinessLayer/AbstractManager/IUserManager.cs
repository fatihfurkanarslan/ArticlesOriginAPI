using Entities;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AbstractManager
{
    interface IUserManager
    {
        Task<List<UserMiniModel>> GetSearchedUsers(Dictionary<string, string> searchItems);
    }
}
