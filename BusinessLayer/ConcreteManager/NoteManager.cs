using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Entities;
using System.Linq;
using Helper;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.SignalR;

namespace BusinessLayer
{
    public class NoteManager
    {
        //IRepository<Note> noteRepository;
        //IRepository<Tag> tagRepository;
        IUnitOfWork unitOfWork;

        IHubContext<NotificationHub> notificationHub;


        public NoteManager(IUnitOfWork _unitOfWork, IHubContext<NotificationHub> _notificationHub)
        {
            unitOfWork = _unitOfWork;
            notificationHub = _notificationHub;
            //noteRepository = _noteRepository;
            //tagRepository = _tagRepository;
        }

         public async Task<PagedList<Note>> GetNotes(NoteParams noteParams)
        {
            var returnValues = await unitOfWork.Note.IncludeAsyncForNote(noteParams, x=> x.Photos);
            //it should be queryable due to pagedlist 
            // var notes = returnValues.Result.AsQueryable();
            //return await PagedList<Note>.CreateAsync(notes, noteParams.PageNumber, noteParams.PageSize);
            return returnValues;
        }

        public async Task<PagedList<Note>> GetNotesByDescending(NoteParams noteParams)
        {
            var returnValues = await unitOfWork.Note.IncludeAsyncForNoteByDescending(noteParams, x => x.Photos, x => x.Id);
            //it should be queryable due to pagedlist 
            // var notes = returnValues.Result.AsQueryable();
            //return await PagedList<Note>.CreateAsync(notes, noteParams.PageNumber, noteParams.PageSize);
            return returnValues;
        }


        public List<Note> GetPopsularNotes()
        {
            var returnValues = unitOfWork.Note.FindPopularNotes(null, "Comments", "Likes", "Photos").Result;
            return returnValues;
        }

        public List<Note> GetNotesByCategory(int id)
        {
            var returnValues = unitOfWork.Note.FindList(x => x.CategoryId == id, "Photos").Result;
            return returnValues;
        }

        public List<Note> GetNotesByUser(string id)
        {
            var returnValues = unitOfWork.Note.FindList(x => x.UserId == id);
            return returnValues;
        }


        public async Task<Note> GetNote(int id)
        {
            var returnValue = await unitOfWork.Note.GetIncludeAsync(x => x.Id == id, "Comments", "Likes", "Tags");
            return returnValue;
        }

        public async Task<int> Insert(Note note)
        {
            int result = await unitOfWork.Note.Insert(note);

            if((result is 1) && (note.IsDraft is true ))
            {

              // notificationHub.Clients.User.SendAsync("", "messega");

            }

            return result;
        }

        public async Task<int> Update(Note note)
        {
            return await unitOfWork.Note.Update(note);
        }

        public async Task<int> Delete(Note note)
        {
            note.Deleted = true;
            return await unitOfWork.Note.UpdateDeleteColumn(note);
        }

        public async Task<List<Note>> GetNotesbyTag(string tag)
        {
            // var returnValues = await tagRepository.IncludeAsync(x => x.NoteId == id);
            var returnValues = unitOfWork.Note.IncludeSingleAsync(tag);
            return await returnValues;
        }
    }
}
