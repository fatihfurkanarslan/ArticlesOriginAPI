using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserDetailModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
