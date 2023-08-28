using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class GoogleUserDto
    {
        public string Id { get; set; }
        public string IdToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Provider { get; set; }
        public string Email { get; set; }

    }


}
