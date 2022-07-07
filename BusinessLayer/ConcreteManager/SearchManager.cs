using DataAccessLayer.UnitOfWork;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ConcreteManager
{
    public class SearchManager
    {

        IUnitOfWork unitOfWork;
        public SearchManager(IUnitOfWork _unitOfWork)
        {
    
            unitOfWork = _unitOfWork;
        }


        public List<Tag> GetNotesByTag(string tag)
        {
            var returnValues = unitOfWork.Tag.FindList(x => x.Tags == tag, "Note").Result;
            return returnValues;
        }


        public List<User> GetUsersByUsername(string username)
        {
            var returnValues = unitOfWork.User.FindList(x => x.UserName == username, "Note").Result;
            return returnValues;
        }

    }
}
