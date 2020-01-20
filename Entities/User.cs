using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }

        //should be guid type
        public Guid ActivatedGuid { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime OnCreated { get; set; }
        public DateTime OnModified { get; set; }
        public string ModifiedUsername { get; set; }
        public string Photourl { get; set; }

        // Bir kullanıcının birçok beğenisi yorumu ve yazısı var.
        public IList<Note> Notes { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<Like> Likes { get; set; }
        

    }
}
