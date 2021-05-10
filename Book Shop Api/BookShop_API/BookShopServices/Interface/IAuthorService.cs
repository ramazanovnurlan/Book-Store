using BookShop_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.BookShopServices.Interface
{
    public interface IAuthorService
    {
        void CreateAuthor(Author author, ref int errorCode, ref string result);

        void DeleteAuthorByIdAsync(int authorId, ref int errorCode, ref string result);


        Author GetAuthorById(int authorId);

        void UpdateAuthor(int id,Author author, ref int errorCode, ref string result);

        List<Author> GetAuthors();
        AuthorCategory GetAuthorCategory(int id);
    }
}
