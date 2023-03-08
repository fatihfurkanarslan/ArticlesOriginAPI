using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserMiniModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string PhotoUrl { get; set; }

        public bool isFollowed { get; set; }

    }
}
