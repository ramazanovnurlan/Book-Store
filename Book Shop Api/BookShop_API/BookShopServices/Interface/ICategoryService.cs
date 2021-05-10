using BookShop_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.BookShopServices.Interface
{
   public interface ICategoryService
    {
        void CreateCategory(Category category, ref int errorCode, ref string result);

        void DeleteCategoryByIdAsync(int categoryId, ref int errorCode, ref string result);


        Category GetCategoryById(int categoryId);

        void UpdateCategory(int id, Category category, ref int errorCode, ref string result);

        List<Category> GetCategories();
    }
}
