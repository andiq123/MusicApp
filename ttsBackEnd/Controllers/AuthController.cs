using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ttsBackEnd.Dto;
using ttsBackEnd.Models;
using ttsBackEnd.Services;

namespace ttsBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this._repo = repo;
            this._config = config;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserForRegisterDto userDto)
        {
            if (userDto == null) return BadRequest("Empty user");
            userDto.Username = userDto.Username.ToLower();
            var userAlreadyExist = await _repo.UserExists(userDto.Username);
            if (userAlreadyExist) return BadRequest("User Already Exists");
            var userToCreate = new User();
            userToCreate.Username = userDto.Username;
            var userCreated = await _repo.Register(userToCreate, userDto.Password);
            if (!await _repo.SaveAll()) return BadRequest("couldn't register this user");
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForLoginDto userToLogin)
        {
            var userFromRepo = await _repo.Login(userToLogin.Username.ToLower(), userToLogin.Password);
            if (userFromRepo == null) return NotFound("Username or password incorrect");
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.ID.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
                     };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }

        [HttpPost("checkexists/{username}")]
        public async Task<IActionResult> CheckUser(string username)
        {
            var userAlreadyExist = await _repo.UserExists(username);
            if (userAlreadyExist) return BadRequest("User Already Exists");
            return Ok(!userAlreadyExist);
        }
    }
}