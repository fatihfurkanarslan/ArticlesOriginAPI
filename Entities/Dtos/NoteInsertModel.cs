using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Entities.Dtos
{
    public class NoteInsertModel
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string Text { get; set; }

        public IFormFile File { get; set; }

        public List<string> Tags { get; set; }

        public bool isDraft { get; set; }

        public string MainPhotourl { get; set; }
        // public string PhotoUrl { get; set; }
        // public List<string> Photos { get; set; }

        // public string PublicId { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }

        // public List<Photo> MyProperty { get; set; }

    }
}