using BookShop_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookShop_API.BookShopServices.Implementation.BookService;

namespace BookShop_API.BookShopServices.Interface
{
    public interface IBookService
    {
        void CreateBook(Book book,string booklang,List<string> authors,List<string> categories, ref int errorCode, ref string result);

        void DeleteBookById(int bookId, ref int errorCode, ref string result);

        Book GetBookById(int BookId,int userid);

        void UpdateBook(int id,Book book,string language, List<string> authors, List<string> categories, ref int errorCode, ref string result);

        List<Book> GetUserBooks(int currentUserId);
        List<Book> GetBooks();
        List<myOption> GetFiltersBook(FilterOption filterOption);
    }
}
