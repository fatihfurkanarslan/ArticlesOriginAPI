using Microsoft.Extensions.Configuration;
using Google.Apis.Auth;
using System;
using System.Threading.Tasks;
using Entities.Dtos;
using Helper.JWTToken;
using DataAccessLayer.UnitOfWork;
using BusinessLayer.AbstractManager;
using NuGet.Common;
using System.Collections.Generic;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BusinessLayer.ConcreteManager
{
    public class GoogleIdTokenValidationService : IGoogleIdTokenValidationService
    {
        private readonly IConfiguration _configuration;
        private readonly JWTCreater _jwtCreater;
        private readonly IUnitOfWork _unitOfWork;

        public GoogleIdTokenValidationService(
            IConfiguration configuration,
            JWTCreater jwtCreater,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _jwtCreater = jwtCreater;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> ValidateIdTokenAsync(ExternalAuthDto model, GoogleUserDto userDto)
        {
            try
            {
                var validationSettings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string> { _configuration["ExternalLogin:Google-Client-Id"] }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, validationSettings);

                if (payload != null)
                {
                    var user = await _unitOfWork.User.GetAsync(x => x.Email == payload.Email);
                    if (user == null)
                    {
                        // Kullanıcı kaydedilmediyse kaydetme işlemini burada gerçekleştirin
                        var newUser = new User
                        {
                            UserName = payload.Name,
                            Email = payload.Email,
                            FirstName = userDto.FirstName,
                            LastName = userDto.LastName,
                            IsActive = true,
                            Photourl = userDto.PhotoUrl,
                            IsAdmin = false,
                            OnCreated = DateTime.UtcNow,
                            OnModified = DateTime.UtcNow        
                        };

                        var createdUser = await _unitOfWork.User.Insert(newUser);

                        // Yeni kullanıcı kaydedildi mi kontrol et
                        
                        user = await _unitOfWork.User.GetAsync(x => x.Email == payload.Email);
                        

                        var loginInfo = new UserLoginInfo(model.Provider, payload.Subject, model.Provider);
                        var addLoginResult = await _unitOfWork.User.AddLoginAsync(user, loginInfo);
                        if (addLoginResult.Succeeded)
                        {
                            // Harici giriş bilgisi başarıyla eklendi
                            var token = _jwtCreater.CreateJWT(user);
                            return token;
                        }

                    }
                    else
                    {
                        var token = _jwtCreater.CreateJWT(user);
                        return token;
                    }

                    // Kullanıcıya giriş bilgisini ekle
                    
                      
                }

                return null;
            }
            catch (Exception ex)
            {
                // Loglama yapılabilir
                return null;
            }
        }




        //public Task<Token> ValidateIdTokenAsync(GoogleLoginModel model)
        //{
        //    throw new NotImplementedException();
        //}
    }
}