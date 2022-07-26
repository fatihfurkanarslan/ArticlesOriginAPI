using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class Note
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title field is a required.")]
        [MaxLength(60, ErrorMessage = "Maximum length is 60 characters for title you text")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }

        public string RAWText { get; set; }
        public int LikeCount { get; set; }
        public DateTime OnCreated { get; set; }
        public DateTime OnModified { get; set; }
        public string OnModifiedUsername { get; set; }
        public bool IsDraft { get; set; }
        public string MainPhotourl { get; set; }

        public IList<Comment> Comments { get; set; }
        public IList<Like> Likes { get; set; }
        public IList<Photo> Photos { get; set; }
        public IList<Tag> Tags { get; set; }

        public User User { get; set; }
        public string? UserId { get; set; }

        public Category Category { get; set; }
        public int? CategoryId { get; set; }
    }
}
