using BusinessLayer.ConcreteManager;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : Controller
    {
        private readonly FollowerManager followerManager;

        public FollowerController(FollowerManager _followerManager)
        {
               followerManager = _followerManager;
        }

        [HttpPost("follow")]
        public async Task<IActionResult> FollowUser(Follower follower)
        {
            int result = await followerManager.InsertFollower(follower);
            return StatusCode(201);
        }
    }
}
