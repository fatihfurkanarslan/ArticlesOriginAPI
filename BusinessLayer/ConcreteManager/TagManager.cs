using DataAccessLayer;
using DataAccessLayer.UnitOfWork;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TagManager
    {
        //IRepository<Tag> tagRepository;
        IUnitOfWork unitOfWork;
        public TagManager(IUnitOfWork _unitOfWork)
        {
            // tagRepository = _tagRepository;
            unitOfWork = _unitOfWork;
        }

        public List<Tag> GetTags(int id)
        {
            // var returnValues = await tagRepository.IncludeAsync(x => x.NoteId == id);
            var returnValues = unitOfWork.Tag.FindList(x => x.NoteId == id);
            return returnValues;
        }


        public async Task<int> Insert(Tag tag)
        {
            return await unitOfWork.Tag.Insert(tag);
        }


        //public async Task<int> Delete(Comment comment)
        //{
        //    return await commentRepository.Remove(comment);
        //}
    }
}
