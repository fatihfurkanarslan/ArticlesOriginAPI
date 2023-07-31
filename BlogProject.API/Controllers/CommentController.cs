using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using BusinessLayer.AbstractManager;
using Entities;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.API.Controllers
{ 

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly CommentManager commentManager;
        private readonly IMapper mapper;

        public CommentController(CommentManager _commentManager, IMapper _mapper)
        {
            commentManager = _commentManager;
            mapper = _mapper;
        }

        [HttpGet("getcomment/{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await commentManager.GetComment(id);

            // var categoryToReturn = mapper.Map<UserDetailModel>(category);

            return Ok(comment);
        }

        [HttpGet("getcomments/{id}")]
        public async Task<IActionResult> GetComments(int id)
        {
            var comments = await commentManager.GetComments(id);

            // var categoryToReturn = mapper.Map<UserDetailModel>(category);

            return Ok(comments);
        }

        //[Authorize]
        [HttpPost("insert")]
        public async Task<IActionResult> InsertNote(CommentInsertModel commentModel)
        {
            var insertValue = mapper.Map<Comment>(commentModel);

            await commentManager.Insert(insertValue);

            return StatusCode(201);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {

            Comment comment = await commentManager.GetComment(id);
            await commentManager.Delete(comment);

            return StatusCode(201);
        }

        [HttpPost("update")]
        public async Task<IActionResult> DeleteComment(CommentUpdateModel commentModel)
        {

            Comment comment = await commentManager.GetComment(commentModel.Id);

            await commentManager.Delete(comment);

            return StatusCode(201);
        }
    }
}