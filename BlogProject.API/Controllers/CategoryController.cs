using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CategoryController : Controller
    {
        private readonly CategoryManager categoryManager;
        private readonly IMapper mapper;

        private readonly IOptions<CloudinarySettings> cloudinaryConfig;
        private Cloudinary _cloudinary;

        public CategoryController(CategoryManager _categoryManager, IMapper _mapper, IOptions<CloudinarySettings> _cloudinaryConfig)
        {
            categoryManager = _categoryManager;
            mapper = _mapper;

            cloudinaryConfig = _cloudinaryConfig;

            Account acc = new Account(
             _cloudinaryConfig.Value.CloudName,
             _cloudinaryConfig.Value.ApiKey,
             _cloudinaryConfig.Value.ApiSecret
         );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("getcategory/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await categoryManager.GetCategory(id);

            //var categoryToReturn = mapper.Map<CategoryUpdateModel>(category);

            return Ok(category);
        }

        [HttpGet("getcategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await categoryManager.GetCategories();
             
           // var categoryToReturn = mapper.Map<UserDetailModel>(category);

            return Ok(categories);
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertCategory([FromForm]CategoryInsertModel model)
        {           

            //var file = model.File;

            //var uploadResult = new ImageUploadResult();

            //if (file.Length > 0)
            //{
            //    using (var stream = file.OpenReadStream())
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


            var insertValue = mapper.Map<Category>(model);

            var result = await categoryManager.Insert(insertValue);

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id < 0) BadRequest("Invalid category id");
 
            Category category = await categoryManager.GetCategory(id);
            await categoryManager.Delete(category);

            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory([FromForm]CategoryUpdateModel categoryModel)
        {

            var file = categoryModel.File;

            var uploadResult = new ImageUploadResult();

            if (file != null)
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

                categoryModel.PhotoUrl = uploadResult.Uri.ToString();
            }


            if (categoryModel != null)
            {
                Category category = await categoryManager.GetCategory(categoryModel.Id);

                category.Categoryname = categoryModel.Categoryname;
                category.Description = categoryModel.Description;
                category.PhotoUrl = categoryModel.PhotoUrl;

                await categoryManager.Update(category);

            }

            return Ok();
        }

      
    }
}