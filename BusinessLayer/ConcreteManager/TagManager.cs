using DataAccessLayer;
using DataAccessLayer.UnitOfWork;
using Entities;
using Entities.Dtos;
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

 


        public async Task<int> Insert(TagInsertModel tagInsertModel)
        {
            List<Note> notes = new List<Note>();
            List<Tag> tagsToAdd = new List<Tag>();   


            if (tagInsertModel.NoteId != 0)
            {
                Note note = await unitOfWork.Note.GetIncludeAsync(x => x.Id == tagInsertModel.NoteId);
                notes.Add(note);
            }

            for (int i = 0; i < tagInsertModel.tags.Count; i++)
            {
                Tag tag = new Tag() {
                    Notes = notes,
                    OnCreated = DateTime.Now,
                    OnModifiedUsername = "user",
                    Tags = tagInsertModel.tags[i]
                };
               tagsToAdd.Add(tag);
            }
            return await unitOfWork.Tag.InsertRange(tagsToAdd);
        }


        //public async Task<int> Delete(Comment comment)
        //{
        //    return await commentRepository.Remove(comment);
        //}
    }
}
