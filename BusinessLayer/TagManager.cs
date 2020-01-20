using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TagManager
    {
        IRepository<Tag> tagRepository;
        public TagManager(IRepository<Tag> _tagRepository)
        {
            tagRepository = _tagRepository;
        }

        public List<Tag> GetTags(int id)
        {
            // var returnValues = await tagRepository.IncludeAsync(x => x.NoteId == id);
            var returnValues = tagRepository.FindList(x => x.NoteId == id);
            return returnValues;
        }

        public List<Tag> GetNotesByTag(string str)
        {
            var returnValues = tagRepository.FindList(x => x.Tags == str, "Note").Result;
            return returnValues;
        }


        public async Task<int> Insert(Tag tag)
        {
            return await tagRepository.Insert(tag);
        }


        //public async Task<int> Delete(Comment comment)
        //{
        //    return await commentRepository.Remove(comment);
        //}
    }
}
