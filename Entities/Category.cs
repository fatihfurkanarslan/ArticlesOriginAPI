using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Categoryname { get; set; }
        public string Description { get; set; }
        public DateTime OnModified { get; set; }
        public string OnModifiedUsername { get; set; }
        public string PhotoUrl { get; set; }
        public string PublicId { get; set; }


        public IList<Note> Notes { get; set; }
    }
}
