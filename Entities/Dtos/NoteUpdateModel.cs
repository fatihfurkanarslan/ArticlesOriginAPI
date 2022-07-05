using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class NoteUpdateModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Text { get; set; }

        public string MainPhotourl { get; set; }

        public string Description { get; set; }

        public bool isDraft { get; set; }
    }
}