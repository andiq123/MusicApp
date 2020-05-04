using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ttsBackEnd.Services;

namespace ttsBackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            this._repo = repo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            if (users.Length == 0) return NotFound("No User found");
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _repo.GetUser(userId);
            if (user == null) return NotFound("No User found");
            return Ok(user);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var userDeletedSuccessFull = await _repo.DeleteUser(userId);
            if (!userDeletedSuccessFull) return NotFound("No user found to delete");
            if (!await _repo.SaveAll()) return BadRequest("couldn't delete user");
            return Ok("User deleted");
        }

        [HttpPost("updateLastOnline")]
        public async Task<IActionResult> UpdateLastOnline()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var updateSuccessfully = await _repo.UpdateLastOnline(userId);
            if (!updateSuccessfully) return NotFound("No User found to update");
            if (!await _repo.SaveAll()) return BadRequest("couldn't update user");
            return Ok("Last online updated succesfully");
        }
    }
}