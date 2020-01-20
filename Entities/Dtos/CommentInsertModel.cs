using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class CommentInsertModel
    {
        public int Id { get; set; }
        public DateTime OnCreated { get; set; }
        public DateTime OnModified { get; set; }
        public string Text { get; set; }

        public int UserId { get; set; }

        public int NoteId { get; set; }
    }
}
