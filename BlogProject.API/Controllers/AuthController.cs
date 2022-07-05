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
using Helper.JWTToken;
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
           private readonly JWTCreater jwtCreater;

        public AuthController(UserManager manager, JWTCreater _jwtCreater)
        {
            userManager = manager;
            jwtCreater = _jwtCreater;
           
        }

        [HttpPost("register")]
         public async Task<ActionResult> Register(UserRegisterModel userModel){

         var result = await userManager.Register(userModel);

            return Ok(result);
        
        }
        
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
                
            string token = jwtCreater.CreateJWT(user);

            return Ok(token);       
        }

    }
}