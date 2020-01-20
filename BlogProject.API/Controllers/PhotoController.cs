using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Entities;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlogProject.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : Controller
    {
         private readonly PhotoManager photoManager;
        private readonly IMapper mapper;
        private readonly IOptions<CloudinarySettings> cloudinaryConfig;
        private Cloudinary _cloudinary;


        public PhotoController(PhotoManager _photoManager, IMapper _mapper, IOptions<CloudinarySettings> _cloudinaryConfig)
        {
            photoManager = _photoManager;
           // userManager = _userManager;
            mapper = _mapper;
            cloudinaryConfig = _cloudinaryConfig;

               Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("getphoto/{userId}")]
        public async Task<IActionResult> GetPhoto(int userId)
        {

            var photo = await photoManager.GetPhoto(userId);

            // var categoryToReturn = mapper.Map<UserDetailModel>(category);

            return Ok(photo);
        }

        [HttpGet("getphotos")]
        public async Task<IActionResult> GetPhotos()
        {

            var photos = await photoManager.GetPhotos();

            // var categoryToReturn = mapper.Map<UserDetailModel>(category);

            return Ok(photos);
        }

        [HttpPost("insert")]
        public ActionResult AddPhotoForUser([FromForm]PhotoForCreationModel photoForCreationModel)
        {     

            var file = photoForCreationModel.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationModel.PhotoUrl = uploadResult.Uri.ToString();
            photoForCreationModel.PublicId = uploadResult.PublicId;
            

            var photo = mapper.Map<Photo>(photoForCreationModel);
            photoManager.Insert(photo);

           
            // var photoToReturn = mapper.Map<PhotoForCreationModel>(photo);
            //return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            
            return Ok(photoForCreationModel.PhotoUrl);
            //return BadRequest("Could not add the photo");
        }

        [HttpPost("insertphotonote")]
        public ActionResult AddPhotoForNote([FromForm]PhotoForCreationModel photoForCreationModel)
        {

            var file = photoForCreationModel.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationModel.PhotoUrl = uploadResult.Uri.ToString();
            photoForCreationModel.PublicId = uploadResult.PublicId;


            var photo = mapper.Map<Photo>(photoForCreationModel);

            photoManager.Insert(photo);


            // var photoToReturn = mapper.Map<PhotoForCreationModel>(photo);
            //return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);

            return Ok(photoForCreationModel.PhotoUrl);
            //return BadRequest("Could not add the photo");
        }





    }
}