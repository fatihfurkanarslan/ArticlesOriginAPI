using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.AbstractManager;
using DataAccessLayer.UnitOfWork;
using Entities;
using Entities.Dtos;
using Helper.JWTToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using NuGet.Protocol;
using static BusinessLayer.UserManager;

namespace BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager userManager;
        private readonly JWTCreater jwtCreater;
        readonly IGoogleIdTokenValidationService _googleIdTokenValidationService;

        public AuthController(UserManager manager, JWTCreater _jwtCreater, IGoogleIdTokenValidationService googleIdTokenValidationService)
        {
            userManager = manager;
            jwtCreater = _jwtCreater;
            _googleIdTokenValidationService = googleIdTokenValidationService;

        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterModel userModel)
        {

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

            if (user == null)
            {
                return Unauthorized("wrong username or password");
                //return BadRequest("Kullanýcý adý veya þifre hatalý.");
            }
            else
            {
                string token = jwtCreater.CreateJWT(user);

                return Ok(token);
            }


        }


        //[HttpPost]
        //public async Task<IActionResult> Login(GoogleLoginModel model)
        //{
        //    Token token = await _googleIdTokenValidationService.ValidateIdTokenAsync(model);
        //    return Ok(token);
        //}

        [HttpPost("externallogin")]
        public async Task<IActionResult> ExternalLogin(GoogleUserDto user)
        {
            ExternalAuthDto externalAuth = new ExternalAuthDto();
            externalAuth.Provider = user.Provider;
            externalAuth.IdToken = user.IdToken;
            string token = await _googleIdTokenValidationService.ValidateIdTokenAsync(externalAuth, user);
        
            //var payload = await jwtcreater.verifygoogletoken(externalauth);
            //if (payload == null)
            //    return badrequest("ýnvalid external authentication.");
            //var info = new userloginýnfo(externalauth.provider, payload.subject, externalauth.provider);
            //var user = await usermanager.findbyloginasync(info.loginprovider, info.providerkey);
            //if (user == null)
            //{
            //    user = await usermanager.findbyemailasync(payload.email);
            //    if (user == null)
            //    {
            //        user = new user { email = payload.email, username = payload.email };
            //        await usermanager.register(user);
            //        //prepare and send an email for the email confirmation
            //        await usermanager.addtoroleasync(user, "viewer");
            //        await usermanager.addloginasync(user, info);
            //    }
            //    else
            //    {
            //        await usermanager.addloginasync(user, info);
            //    }
            //}
            //if (user == null)
            //    return badrequest("ýnvalid external authentication.");
            ////check for the locked out account
            //var token = await jwtcreater.createjwt(user);
            //return ok(new authresponsedto { token = token, ýsauthsuccessful = true });
            return Ok(token);
        }

    }
}