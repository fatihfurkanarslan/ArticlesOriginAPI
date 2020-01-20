using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime OnCreated { get; set; }
        public DateTime OnModified { get; set; }
        public string OnModifiedUsername { get; set; }
        public string PublicId { get; set; }

        public Note Note { get; set; }
        public int? NoteId { get; set; }
    }
}
