using AutoMapper;
using BusinessLayer.ConcreteManager;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogProject.API.Controllers
{
    public class SearchController : Controller
    {

        private readonly SearchManager searchManager;
        private readonly IMapper mapper;

        public SearchController(SearchManager _searchManager, IMapper _mapper)
        {
            searchManager = _searchManager;
            mapper = _mapper;
        }
        [HttpPost("getnotesbytag")]
        public IActionResult GetNotesByTag(string wordToSearch)
        {
            List<Tag> tags = searchManager.GetNotesByTag(wordToSearch);

            return Ok(tags);
        }


        [HttpPost("getusersbyusername")]
        public IActionResult getUsersByUsername(string wordToSearch)
        {
            List<User> tags = searchManager.GetUsersByUsername(wordToSearch);

            return Ok(tags);
        }
    }
}
