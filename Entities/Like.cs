using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Like
    {
        public int Id { get; set; }
        public DateTime OnCreated { get; set; }
        public string OnModifiedUsername { get; set; }

        public User User { get; set; }
        public int? UserId { get; set; }

        public Note Note { get; set; }
        public int? NoteId { get; set; }
    }
}
