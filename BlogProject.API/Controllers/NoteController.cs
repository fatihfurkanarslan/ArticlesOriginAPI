using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Entities;
using Entities.Dtos;
using Helper;
using Helper.Filters;
using Helper.Logging;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly NoteManager noteManager;
        private readonly IMapper mapper;
        //LoggerManager includes Nlog 
        private ILoggerManager logger;

        private readonly IOptions<CloudinarySettings> cloudinaryConfig;
        private Cloudinary _cloudinary;

        public NoteController(NoteManager _noteManager, IMapper _mapper, IOptions<CloudinarySettings> _cloudinaryConfig, ILoggerManager _logger)
        {
            noteManager = _noteManager;
            mapper = _mapper;
            logger = _logger;

            cloudinaryConfig = _cloudinaryConfig;

            Account acc = new Account(
             _cloudinaryConfig.Value.CloudName,
             _cloudinaryConfig.Value.ApiKey,
             _cloudinaryConfig.Value.ApiSecret
         );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("getnotesbycategory/{id}")]
        public IActionResult GetNoteByCategory(int id)
        {
            var notes = noteManager.GetNotesByCategory(id);

            // var categoryToReturn = mapper.Map<UserDetailModel>(category);

            return Ok(notes);
        }


        [HttpGet("getnotesbyuser/{id}")]
        public IActionResult GetNoteByUser(int id)
        {

            var notes = noteManager.GetNotesByUser(id);

            // var categoryToReturn = mapper.Map<UserDetailModel>(category);

            return Ok(notes);
        }

        //if it is public location, whenever cache duration is over, all new responses are added to public caching location,
        //if it is not, responses store private location 
        //cache location private that means allow to cache response
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        //Once the cache expires, refuse to return stale responses to the user even if they say that stale responses are acceptable.
        [HttpCacheValidation(MustRevalidate = true)]
        [HttpGet("getnotes")]
        public async Task<IActionResult> GetNotes([FromQuery]NoteParams noteParams)
        {
            //logging test
            logger.LogInfo("it works!!");
            var notes = await noteManager.GetNotesByDescending(noteParams);

            Response.AddPagination(notes.CurrentPage, notes.PageSize, notes.TotalCount, notes.TotalPages);

            // var categoryToReturn = mapper.Map<UserDetailModel>(category);

            return Ok(notes);
        }

        [HttpGet("getnote/{id}")]
        public async Task<IActionResult> GetNote(int id)
        {
            var note = await noteManager.GetNote(id);

            // var categoryToReturn = mapper.Map<UserDetailModel>(category);

            return Ok(note);
        }
        
        //this is just a header which controls duration. It is not a cache store.
        [ResponseCache(CacheProfileName = "120SecondsDuration")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = true)]
        [HttpGet("getpopularnotes")]
        public IActionResult GetPopularNotes()
        {   
            var popularNotes = noteManager.GetPopsularNotes();

            // var categoryToReturn = mapper.Map<UserDetailModel>(category);

            return Ok(popularNotes);
        }

        [HttpPost("draft")]
        public async Task<IActionResult> DraftNote(NoteInsertModel noteModel)
        {
            var insertValue = mapper.Map<Note>(noteModel);

            await noteManager.Insert(insertValue);

            return Ok(insertValue.Id);
        }

        //modelstate.isvalid and errorhandling attribute
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("insert")]
        public async Task<IActionResult> InsertNote(NoteInsertModel noteModel)
        {

            #region Photos
            // var photos = noteModel.Photos;

            //string x = photos[0].Replace("data:image/jpeg;base64,", "");
            //byte[] imageBytes = Convert.FromBase64String(x);

            //MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            //ms.Write(imageBytes, 0, imageBytes.Length);

            //System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);

            //var path = Path.Combine(_hostingEnvironment.WebRootPath, "Sample.PNG");
            //image.Save(Server.MapPath("~/image.png"), System.Drawing.Imaging.ImageFormat.Png);
            //ImageUploadParams uploadParams = new ImageUploadParams()
            //{
            //    File = new FileDescription(Server.MapPath("~/image.png")),
            //    PublicId = User.Identity.Name + DateTime.Now + "demotevators"
            //};
            //uploadResult = _cloudinary.Upload(uploadParams);
            //System.IO.File.Delete(Server.MapPath("~/image.png"));


            //byte[] imageBytes = Convert.FromBase64String(photos[0]);

            //var uploadResult = new ImageUploadResult();

            //if (photos.Count > 0)
            //{
            //    using (var stream = imageBytes.OpenReadStream())
            //    {
            //        var uploadParams = new ImageUploadParams()
            //        {
            //            File = new FileDescription(file.Name, stream),
            //            Transformation = new Transformation()
            //                .Width(500).Height(500).Crop("fill").Gravity("face")
            //        };

            //        uploadResult = _cloudinary.Upload(uploadParams);
            //    }
            //}

            //model.PhotoUrl = uploadResult.Uri.ToString();
            //model.PublicId = uploadResult.PublicId;

            //////////////////////

            #endregion

            var insertValue = mapper.Map<Note>(noteModel);

            await noteManager.Insert(insertValue);

            return StatusCode(201);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {

            Note note = await noteManager.GetNote(id);
            await noteManager.Delete(note);

            return Ok();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateNote([FromBody]NoteUpdateModel noteModel)
        {
            Note note = await noteManager.GetNote(noteModel.Id);

                note.MainPhotourl = noteModel.MainPhotourl;

                note.Title = noteModel.Title;
                note.Text = noteModel.Text;
                note.IsDraft = noteModel.isDraft;
                note.Description = noteModel.Description;

                int result = await noteManager.Update(note);

                return Ok(result);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("updateImage")]
        public async Task<IActionResult> UpdateNoteImage(NoteUpdateModel noteModel)
        {
            Note note = await noteManager.GetNote(noteModel.Id);

            if (note == null)
            {
                return StatusCode(400);
            }
            else
            {

                note.MainPhotourl = noteModel.MainPhotourl;          

                int result = await noteManager.Update(note);

                return Ok(result);
            }
        }

    }
}