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
    public class AuthorService:IAuthorService
    {
        AppConfiguration config = new AppConfiguration();
        private readonly IRepository<Author> _authors;
        private readonly IRepository<Errors> _errors;
        private readonly IRepository<BookAuthor> _bookauthor;
        private readonly IRepository<BookCategory> _bookcategory;
        private readonly IRepository<Category> _category;

        //    private readonly ILoggerManager _logger;


        public AuthorService(IRepository<Author> authors,
            IRepository<Errors> errors, IRepository<BookAuthor> bookauthor, IRepository<BookCategory> bookcategory, IRepository<Category> category
            )
        {
            _authors = authors;
            _category = category;
            _errors = errors;
            _bookauthor = bookauthor;
            _bookcategory = bookcategory;
           
        }

        public void CreateAuthor(Author author, ref int errorCode, ref string result)
        {
             _authors.Insert(author);
            _authors.Save();
            result = "Uğurla başa çatdı!";
            errorCode = 0;
        }

        public void DeleteAuthorByIdAsync(int authorId, ref int errorCode, ref string result)
        {
           Author author= _authors.FindBy(x => x.Id == authorId).FirstOrDefault();
            if(author==null)
            {
                result = "Silmek ucun lazimi melumat tapilmadi!";
                errorCode = 0;
            }
            else
            {
                _authors.Remove(author);
                _authors.Save();
                result = "Uğurla başa çatdı!";
                errorCode = 0;
            }
            
        }


        public Author GetAuthorById(int authorId)
        {
            return _authors.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == authorId);
        }

        public void UpdateAuthor(int id,Author author, ref int errorCode, ref string result)
        {
            try
            {
                var oldauthor = _authors.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == id);
                if (oldauthor == null)
                {
                    errorCode = 1;
                    result = "Author tapılmadı!";
                    return;
                }
                oldauthor.FullName = author.FullName;
                oldauthor.Biography = author.Biography;
                _authors.Update(oldauthor);
                _authors.Save();
                errorCode = 0;
                result = "Uğurla başa çatdı!";
            }
            catch (Exception)
            {
                errorCode = 1;
                result = "Məlumatın sistemə əlavə olunması baş tutmadı!";
            }
        }

        public List<Author> GetAuthors()
        {
            return  _authors.AllQuery.ToList();
        }

        public AuthorCategory GetAuthorCategory(int id)
        {
            AuthorCategory autcat = new AuthorCategory();
            List<BookAuthor> alfas=_bookauthor.AllQuery.Where(x => x.BookId == id).ToList();
            List<BookCategory> betas = _bookcategory.AllQuery.Where(x => x.BookId == id).ToList();
            autcat.authorcount = alfas.Count;
            autcat.categorycount = betas.Count;
            List<string> BookAuthor = new List<string>();
            List<string> BookCategory = new List<string>();
            foreach (var alfa in alfas)
            {
                BookAuthor.Add(_authors.AllQuery.Where(x=>x.Id==alfa.AuthorId).FirstOrDefault().FullName);
            }
            foreach (var beta in betas)
            {
                BookCategory.Add(_category.AllQuery.Where(x=>x.Id==beta.CategoryId).FirstOrDefault().Title);
            }
            autcat.BookAuthor = BookAuthor;
            autcat.BookCategory = BookCategory;
            return autcat;

        }
    }
}
