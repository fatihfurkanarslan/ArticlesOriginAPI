using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Entities;
using Entities.Dtos;

namespace BusinessLayer
{
    public class PhotoManager
    {

        IRepository<Photo> photoManager;
        public PhotoManager(IRepository<Photo> _photoManager)
        {
            photoManager = _photoManager;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var returnValue = await photoManager.GetAsync(x => x.Id == id);
            return returnValue;
        }

        public async Task<List<Photo>> GetPhotos()
        {
            var returnValues = await photoManager.GetListAsync();
            return returnValues;
        }

        public Task<int> Insert(Photo photo)
        {
            var returnValue = photoManager.Insert(photo);
            return returnValue;
        }


    }
}