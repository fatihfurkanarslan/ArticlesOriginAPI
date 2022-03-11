using DataAccessLayer;
using DataAccessLayer.UnitOfWork;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CategoryManager
    {
        //IRepository<Category> categoryRepository;
        IUnitOfWork unitOfWork;
        public CategoryManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            //categoryRepository = _categoryRepository;
        }

         public async Task<List<Category>> GetCategories()
        {
            return await unitOfWork.Category.GetListAsync();         
            
        }

        public async Task<List<Category>> GetCategoriesWithNotes()
        {
            return await unitOfWork.Category.IncludeAsync(x => x.Notes);
            
        }
        public async Task<Category> GetCategory(int id)
        {
            return await unitOfWork.Category.GetAsync(x => x.Id == id);
            
        }

        public async Task<int> Insert(Category category)
        {
            Category cat = await unitOfWork.Category.GetAsync(x => x.Categoryname == category.Categoryname);

            if(cat == null)
            {
               return await unitOfWork.Category.Insert(category);
            }
            else
            {
                return 0;
            }
    
        }

        public async Task<int> Update(Category category)
        {
            return await unitOfWork.Category.Update(category);
        }

        public async Task<int> Delete(Category category)
        {
            return await unitOfWork.Category.Remove(category);
        }

    }
}
