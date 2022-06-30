using BusinessLayer.AbstractManager;
using DataAccessLayer.UnitOfWork;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ConcreteManager
{
    public class FollowerManager : IFollowerManager
    {

        IUnitOfWork unitOfWork;

        public FollowerManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }


        public async Task<int> InsertFollower(Follower follower)
        {
            return await unitOfWork.Follower.Insert(follower);
        }
    }
}
