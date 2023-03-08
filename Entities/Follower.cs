using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Follower
    {

        public int Id { get; set; }

        [ForeignKey("FollowerId")]
        public virtual User UserFollowers { get; set; }
        public string? FollowerId { get; set; }

        [ForeignKey("FolloweeId")]
        public virtual User UserFollowees { get; set; }
        public string? FolloweeId { get; set; }

   

    }
}
