using System;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class PhotoForCreationModel
    {
        public string PhotoUrl { get; set; }
        public IFormFile File { get; set; }
       public DateTime OnCreated { get; set; }
        public DateTime OnModified { get; set; }
        public string OnModifiedUsername { get; set; }
        public string PublicId { get; set; }
        public int NoteId { get; set; }

        public PhotoForCreationModel()
        {
            OnCreated = DateTime.Now;
            OnModified = DateTime.Now;
            OnModifiedUsername = "admin";
        }
    }
}