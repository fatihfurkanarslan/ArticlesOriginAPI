using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.UnitOfWork;
using Entities;
using Entities.Dtos;

namespace BusinessLayer
{
    public class PhotoManager
    {

        //IRepository<Photo> photoManager;
        IUnitOfWork unitOfWork;
        public PhotoManager(IUnitOfWork _unitOfWork)
        {
            //photoManager = _photoManager;
            unitOfWork = _unitOfWork;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var returnValue = await unitOfWork.Photo.GetAsync(x => x.Id == id);
            return returnValue;
        }

        public async Task<List<Photo>> GetPhotos()
        {
            var returnValues = await unitOfWork.Photo.GetListAsync();
            return returnValues;
        }

        public Task<int> Insert(Photo photo)
        {
            var returnValue = unitOfWork.Photo.Insert(photo);
            return returnValue;
        }


    }
}