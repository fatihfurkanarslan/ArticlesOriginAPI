﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using Entities;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager userManager;
        private readonly IMapper mapper;
        public UserController(UserManager _userManager, IMapper _mapper)
        {
            userManager = _userManager;
            mapper = _mapper;
        }

        [HttpGet("getuser/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await userManager.GetUser(id);

            UserDetailModel userToReturn = mapper.Map<UserDetailModel>(user);

            return Ok(userToReturn);
        }

        [HttpGet("getusers/{searchKeyword}")]
        public async Task<IActionResult> GetUsers(string searchKeyword)
        {
            var returnValue = await userManager.GetUsers(searchKeyword);
            
            return Ok(returnValue);
        }

        [HttpGet("getsearchedusers")]
        public async Task<IActionResult> GetSearchedUsers([FromQuery] Dictionary<string, string> searchItems)
        {
            var returnValue = await userManager.GetSearchedUsers(searchItems);

            return Ok(returnValue); 
        }




        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser(UserDetailModel model)
        {
            User user = await userManager.GetUser(model.Id);

            if (user != null)
            {
                user.UserName = model.Username;
                user.FirstName = model.Firstname;
                user.LastName = model.Lastname;
                user.Description = model.Description;
                if(model.PhotoUrl != user.Photourl || model.PhotoUrl != null)
                {
                    user.Photourl = model.PhotoUrl;
                }
       
            }

            var returnValue = await userManager.Update(user);

            return Ok(returnValue);
        }
    }
}