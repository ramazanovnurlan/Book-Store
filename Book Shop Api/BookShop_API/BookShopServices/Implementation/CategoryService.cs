using BookShop_API.BookShop_Repostory.Repostory;
using BookShop_API.BookShopServices.AppConfig;
using BookShop_API.BookShopServices.Interface;
using BookShop_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.BookShopServices.Implementation
{
    public class CategoryService : ICategoryService
    {
        AppConfiguration config = new AppConfiguration();
        private readonly IRepository<Category> _categories;
        private readonly IRepository<Errors> _errors;
        //    private readonly ILoggerManager _logger;


        public CategoryService(IRepository<Category> categories,
            IRepository<Errors> errors
            )
        {
            _categories = categories;
            _errors = errors;
        }
        public void CreateCategory(Category category, ref int errorCode, ref string result)
        {
            _categories.Insert(category);
            _categories.Save();
            result = "Uğurla başa çatdı!";
            errorCode = 0;
        }

        public void DeleteCategoryByIdAsync(int categoryId, ref int errorCode, ref string result)
        {
            Category category = _categories.FindBy(x => x.Id == categoryId).FirstOrDefault();
            if (category == null)
            {
                result = "Silmek ucun lazimi melumat tapilmadi!";
                errorCode = 0;
            }
            else
            {
                _categories.Remove(category);
                _categories.Save();
                result = "Uğurla başa çatdı!";
                errorCode = 0;
            }
        }

        public List<Category> GetCategories()
        {
            return  _categories.AllQuery.ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categories.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == categoryId);
        }

        public void UpdateCategory(int id, Category category, ref int errorCode, ref string result)
        {
            try
            {
                var oldcategory = _categories.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == id);
                if (oldcategory == null)
                {
                    errorCode = 1;
                    result = "Category tapılmadı!";
                    return;
                }
                oldcategory.Title = category.Title;
                _categories.Update(oldcategory);
                _categories.Save();
                errorCode = 0;
                result = "Uğurla başa çatdı!";
            }
            catch (Exception)
            {
                errorCode = 1;
                result = "Məlumatın sistemə əlavə olunması baş tutmadı!";
            }
        }
    }
}
