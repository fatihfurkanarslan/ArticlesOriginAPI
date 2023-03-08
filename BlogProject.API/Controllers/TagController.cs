using System;
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
    public class TagController : Controller
    {
        private readonly TagManager tagManager;
        private readonly NoteManager noteManager;
        private readonly IMapper mapper;

        public TagController(TagManager _tagManager, IMapper _mapper, NoteManager _noteManager)
        {
            tagManager = _tagManager;
            noteManager = _noteManager;
            mapper = _mapper;
        }

   
        [HttpPost("insert")]
        public async Task<IActionResult> InsertTag(TagInsertModel tagModel)
        {
            //string tag;
            //var insertValue = mapper.Map<Tag>(tagModel);
            if (tagModel != null)
            {
                await tagManager.Insert(tagModel);


                //for (int i = 0; i < tagModel.tags.Count; i++)
                //{
                //    // tag = tagModel.tags[i];
                //    Tag tagToInsert = new Tag()
                //    {
                //        Tags = tagModel.tags[i],
                //        OnCreated = DateTime.Now
                //    };
                //    //await tagManager.Insert(insertValue);
                //    await tagManager.Insert(tagToInsert);
                //}

                {
                    //first get notes then insert notes to tags
                };

                return Ok(200);
            }
            
            
            return StatusCode(400);
        }

        //[HttpPost("getnotesbytag")]
        //public async Task<IActionResult> GetNotes(TagModel tag)
        //{
        //    List<Tag> notes = await tagManager.GetTags(tag.tagParam);

        //    return Ok(notes);
        //}

        //[HttpPost("getnotesbytag")]
        //public IActionResult GetNotesByTag(TagModel tag)
        //{    
        //    List<Tag> tags = tagManager.GetNotesByTag(tag.tag);

        //    return Ok(tags);
        //}

    }
}