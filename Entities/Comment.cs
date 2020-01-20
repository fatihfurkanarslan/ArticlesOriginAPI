using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime OnCreated { get; set; }
        public DateTime OnModified { get; set; }
        public string OnModifiedUsername { get; set; }

        public User User { get; set; }
        public int? UserId { get; set; }

        public Note Note { get; set; }
        public int? NoteId { get; set; }

    }
}
