using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class TagInsertModel
    {
        // public int Id { get; set; }

        public int NoteId { get; set; }
        public List<string> tags { get; set; }
    }
}
