using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.Services
{
    public interface ICategoryService
    {
        abstract Task<List<CategoryDTO>> getCategories();
        abstract Task<CategoryDTO> postCategory(CategoryDTO category);
        abstract Task<CategoryDTO> putCategory(CategoryDTO newCategory);
        abstract Task<bool> deleteCategory(int id);

    }
    public class CategoryService:ICategoryService
    {
        private readonly TodoContext _context;

        public CategoryService(TodoContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryDTO>> getCategories()
        {
            var categories = await Task.FromResult(_context.Categories.ToList());
            var categoriesDtO = (from c in categories
                            select new CategoryDTO
                            {
                                categoryID= c.CategoryID,
                                name=c.Name
                            }).ToList();
            return categoriesDtO;
        }
        public async Task<CategoryDTO> postCategory(CategoryDTO newCategory)

        {
            var category = new Category
            {
                Name = newCategory.name
            };
            _context.Categories.Add(category);

            await saveChanges();

            newCategory.categoryID = category.CategoryID;
            return newCategory;
        }

        public async Task<CategoryDTO> putCategory(CategoryDTO newCategory)

        {
            var category = await getCategoryByID(newCategory.categoryID);
            category.Name = newCategory.name;

            await saveChanges();

            return newCategory;
        }
        public async Task<bool> deleteCategory(int id)
        {
            var category = await getCategoryByID(id);
            _context.Categories.Remove(category);

            await saveChanges();

            return true;
        }

        private async Task<Category> getCategoryByID(int categoryID)
        {
            var category = await _context.Categories.FindAsync(categoryID);
            if (category == null)
            {
                throw new Exception("Bad Request");
            }
            return category;
        }
        private async Task<bool> saveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
