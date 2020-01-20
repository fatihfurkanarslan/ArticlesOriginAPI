using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Entities;
using System.Linq;
using Helper;
namespace BusinessLayer
{
    public class NoteManager
    {
        IRepository<Note> noteRepository;
        IRepository<Tag> tagRepository;
        public NoteManager(IRepository<Note> _noteRepository, IRepository<Tag> _tagRepository)
        {
            noteRepository = _noteRepository;
            tagRepository = _tagRepository;
        }

         public async Task<PagedList<Note>> GetNotes(NoteParams noteParams)
        {
            var returnValues = await noteRepository.IncludeAsyncForNote(noteParams, x=> x.Photos);
            //it should be queryable due to pagedlist 
            // var notes = returnValues.Result.AsQueryable();
            //return await PagedList<Note>.CreateAsync(notes, noteParams.PageNumber, noteParams.PageSize);
            return returnValues;
        }

        public async Task<PagedList<Note>> GetNotesByDescending(NoteParams noteParams)
        {
            var returnValues = await noteRepository.IncludeAsyncForNoteByDescending(noteParams, x => x.Photos, x => x.Id);
            //it should be queryable due to pagedlist 
            // var notes = returnValues.Result.AsQueryable();
            //return await PagedList<Note>.CreateAsync(notes, noteParams.PageNumber, noteParams.PageSize);
            return returnValues;
        }


        public List<Note> GetPopsularNotes()
        {
            var returnValues = noteRepository.FindPopularNotes(null, "Comments", "Likes", "Photos").Result;
            return returnValues;
        }

        public List<Note> GetNotesByCategory(int id)
        {
            var returnValues = noteRepository.FindList(x => x.CategoryId == id, "Photos").Result;
            return returnValues;
        }

        public List<Note> GetNotesByUser(int id)
        {
            var returnValues = noteRepository.FindList(x => x.UserId == id);
            return returnValues;
        }


        public async Task<Note> GetNote(int id)
        {
            var returnValue = await noteRepository.GetIncludeAsync(x => x.Id == id, "Comments", "Likes");
            return returnValue;
        }

        public async Task<int> Insert(Note note)
        {
            return await noteRepository.Insert(note);
        }

        public async Task<int> Update(Note note)
        {
            return await noteRepository.Update(note);
        }

        public async Task<int> Delete(Note note)
        {
            return await noteRepository.Remove(note);
        }
    }
}
