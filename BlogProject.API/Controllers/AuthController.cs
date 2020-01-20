using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Entities;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
           private readonly UserManager userManager;
         
           private readonly IConfiguration _config;   

        public AuthController(UserManager manager, IConfiguration config)
        {
            userManager = manager;
            _config = config;
           
        }

        [HttpPost("register")]
         public async Task<ActionResult> Register(UserRegisterModel userModel){

         var result = await userManager.Register(userModel);

            return Ok(result);
        
        }
        
        [Authorize]
        [HttpGet("activateuser/{id}")]
        public async Task<ActionResult> Activate(Guid id)
        {

            await userManager.ActivateUser(id);
            return StatusCode(201);

        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForLoginModel userModel)
        {
            
                var user = await userManager.Login(userModel);

                if (user == null) Unauthorized();                 
                

                var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
                };

                //here creating token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(10),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                //here gives a token as return
                return Ok(new
                {
                    token = tokenHandler.WriteToken(token)
                });     
           
        }

    }
}