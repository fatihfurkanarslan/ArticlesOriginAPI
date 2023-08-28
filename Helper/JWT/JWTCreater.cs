using Entities;
using Entities.Dtos;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Helper.JWTToken
{  
    public class JWTCreater
    {
        IConfiguration config;
        public JWTCreater(IConfiguration _config)
        {
            config = _config;
        }



        public string CreateJWT(User userInfo)
        {

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                new Claim(ClaimTypes.Name, userInfo.UserName)

                // new Claim("UserId", userInfo.Id.ToString()),
                //new Claim("Username", userInfo.UserName)
            };


            //here creating token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(10),
                SigningCredentials = signingCreds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            ////here gives a token as return


            return tokenHandler.WriteToken(token);
          
        }


        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { config["ExternalLogin:Google-Client-Id"] }
                };
            

                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);

                return payload;
            }
            catch (Exception ex)
            {
                //log an exception
                return null;
            }
        }


    }
}
