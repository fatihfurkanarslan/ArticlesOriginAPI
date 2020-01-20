using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
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
        public int? UserId { get; set; }

        public Category Category { get; set; }
        public int? CategoryId { get; set; }
    }
}
