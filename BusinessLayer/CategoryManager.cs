using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CategoryManager
    {
        IRepository<Category> categoryRepository;
        public CategoryManager(IRepository<Category> _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }

         public async Task<List<Category>> GetCategories()
        {
            var returnValues = await categoryRepository.GetListAsync();
            return returnValues;
        }

        public async Task<List<Category>> GetCategoriesWithNotes()
        {
            var returnValues = await categoryRepository.IncludeAsync(x => x.Notes);
            return returnValues;
        }
        public async Task<Category> GetCategory(int id)
        {
            var returnValue = await categoryRepository.GetAsync(x => x.Id == id);
            return returnValue;
        }

        public async Task<int> Insert(Category category)
        {
            Category cat = await categoryRepository.GetAsync(x => x.Categoryname == category.Categoryname);

            if(cat == null)
            {
               return await categoryRepository.Insert(category);
            }
            else
            {
                return 0;
            }
    
        }

        public async Task<int> Update(Category category)
        {
            return await categoryRepository.Update(category);
        }

        public async Task<int> Delete(Category category)
        {
            return await categoryRepository.Remove(category);
        }

    }
}
