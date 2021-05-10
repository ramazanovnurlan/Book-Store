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
    public class BookService : IBookService
    {
        AppConfiguration config = new AppConfiguration();
        private readonly IRepository<Book> _book;
        private readonly IRepository<Language> _language;
        private readonly IRepository<Author> _author;
        private readonly IRepository<Category> _category;
        private readonly IRepository<Errors> _errors;
        private readonly IRepository<BookAuthor> _bookauthor;
        private readonly IRepository<BookCategory> _bookcategory;
        //    private readonly ILoggerManager _logger;


        public BookService(IRepository<Book> book,
            IRepository<Errors> errors, IRepository<Language> language, IRepository<Author> author,IRepository<Category> category,
              IRepository<BookAuthor> bookauthor, IRepository<BookCategory> bookcategory
            )
        {
            _book = book;
            _errors = errors;
            _language = language;
            _author = author;
            _category = category;
            _bookauthor = bookauthor;
            _bookcategory = bookcategory;
        }
        public void CreateBook(Book book, string booklang,List<string>authors, List<string> categories, ref int errorCode, ref string result)
        {
            try
            {
                foreach (var b in _book.AllQuery.ToList())
                {
                    if (b.Title == book.Title)
                    {
                        errorCode = 1;
                        result = "Bu kitab artiq bazada var!";
                        return;
                    }
                }
                var lang = _language.AllQuery.FirstOrDefault(x => x.Title == booklang);
                if (lang == null)
                {
                    errorCode = 1;
                    result = "Dil tapılmadı!";
                    return;
                }
                book.LanguageId = lang.Id;
                //var author = _author.AllQuery.FirstOrDefault(x => x.FullName == authorname);
                //var category = _category.AllQuery.FirstOrDefault(x => x.Title == categorytitle);
                //if (author==null)
                //{
                //    errorCode = 1;
                //    result = "Author tapılmadı!";
                //    return;
                //}
                //if (category == null)
                //{
                //    errorCode = 1;
                //    result = "Category tapılmadı!";
                //    return;
                //}

                _book.Insert(book);
                _book.Save();
                var count=_book.AllQuery.ToList().Count();
                var bookId = _book.All.ToList()[count-1].Id;
                //BookAuthor bookAuthor = new BookAuthor() { AuthorId = author.Id, BookId = bookId };
                //BookCategory bookCategory = new BookCategory() { CategoryId = category.Id ,BookId=bookId};
                //_bookauthor.Insert(bookAuthor);
                //_book.Save();
                //_bookcategory.Insert(bookCategory);
                //_book.Save();
                errorCode = 0;
                result = "Uğurla başa çatdı!";
                if(authors.Count==0 && categories.Count == 0)
                {
                    errorCode = 1;
                    result = "Author ve ya Category tapilmadi";
                    return;
                }
                foreach (var author in authors)
                {
                    var authorid= _author.AllQuery.Where(x => x.FullName == author).FirstOrDefault().Id;
                    BookAuthor bookAuthor = new BookAuthor() { AuthorId = authorid, BookId = bookId };
                    _bookauthor.Insert(bookAuthor);
                    _bookauthor.Save();
                }
                foreach (var category in categories)
                {
                    var categoryid = _category.AllQuery.Where(x => x.Title == category).FirstOrDefault().Id;
                     BookCategory bookCategory = new BookCategory() { CategoryId = categoryid, BookId = bookId };
                    _bookcategory.Insert(bookCategory);
                    _bookauthor.Save();
                }
                errorCode = 0;
                result = "Uğurla başa çatdı!";

            }
            catch (Exception)
            {
                errorCode = 1;
                result = "Məlumatın sistemə əlavə olunması baş tutmadı!";
            }
        }

        public void DeleteBookById(int bookId, ref int errorCode, ref string result)
        {
            Book book = _book.FindBy(x => x.Id == bookId).FirstOrDefault();
            if (book == null)
            {
                result = "Silmek ucun lazimi melumat tapilmadi!";
                errorCode = 0;
            }
            else
            {
                _book.Remove(book);
                _book.Save();
                var bookauthories= _bookauthor.AllQuery.Where(x => x.BookId == bookId).ToList();
                var bookcategories=_bookcategory.AllQuery.Where(x => x.BookId == bookId).ToList();
                _bookauthor.RemoveRange(bookauthories);
                _bookcategory.RemoveRange(bookcategories);
                _bookauthor.Save();
                _bookcategory.Save();
                result = "Uğurla başa çatdı!";
                errorCode = 0;
            }
        }

        public Book GetBookById(int bookid,int userid)
        {
            Book book = _book.AllQuery.FirstOrDefault(x => x.Id == bookid);
            return book;
        }

        public List<Book> GetUserBooks(int id)
        {
           return  _book.AllQuery.Include(x=>x.User).Where(x=>x.UserId==id).ToList();
        }
        public List<Book> GetBooks()
        {
            return _book.AllQuery.OrderByDescending(x=>x.Id).ToList();
        }

        public void UpdateBook(int id, Book book,string langname, List<string> authors, List<string> categories, ref int errorCode, ref string result)
        {
            try
            {
                var oldbookupdate = _book.FindBy(x => x.Id == id).FirstOrDefault();
                oldbookupdate.Imagename = book.Imagename;
                oldbookupdate.Price = book.Price;
                oldbookupdate.ReleaseDate = book.ReleaseDate;
                oldbookupdate.Title = book.Title;
                oldbookupdate.Description = book.Description;
                if (langname == "0")
                {
                    langname = _language.AllQuery.FirstOrDefault(x => x.Id == oldbookupdate.LanguageId).Title;
                }
                var lang = _language.AllQuery.FirstOrDefault(x => x.Title == langname);
                if(langname!=null || langname != "")
                {
                    if (lang == null)
                    {
                        errorCode = 1;
                        result = "Dil tapılmadı!";
                        return;
                    }
                    oldbookupdate.LanguageId = lang.Id;
                    
                }
                result = "Melumat ugurla deyisildi";
               

                //var author = _author.AllQuery.FirstOrDefault(x => x.FullName == authorname);
                //if (authorname!=null || authorname != "")
                //{
                //    if (author == null)
                //    {
                //        errorCode = 1;
                //        result = "Author tapılmadı!";
                //        return;
                //    }
                //    var bookauthor = _bookauthor.AllQuery.Where((x, y) => x.BookId == oldbookupdate.Id && x.AuthorId == author.Id).FirstOrDefault();
                //    bookauthor.AuthorId = author.Id;
                //    _bookauthor.Update(bookauthor);
                //}
                //var category = _category.AllQuery.FirstOrDefault(x => x.Title == categorytitle);
                //if (categorytitle != null || categorytitle != "")
                //{
                //    if (category == null)
                //    {
                //        errorCode = 1;
                //        result = "Category tapılmadı!";
                //        return;
                //    }
                //    var bookcategory = _bookcategory.AllQuery.Where((x, y) => x.BookId == book.Id && x.CategoryId == category.Id).FirstOrDefault();
                //    bookcategory.CategoryId = category.Id;
                //    _bookcategory.Update(bookcategory);

                //}
                _book.Update(oldbookupdate);
                _book.Save();

                var bookauthories = _bookauthor.AllQuery.Where(x => x.BookId == id).ToList();
                var bookcategories = _bookcategory.AllQuery.Where(x => x.BookId == id).ToList();
                _bookauthor.RemoveRange(bookauthories);
                _bookcategory.RemoveRange(bookcategories);
                _bookauthor.Save();
                _bookcategory.Save();
                foreach (var author in authors)
                {
                    var authorid = _author.AllQuery.Where(x => x.FullName == author).FirstOrDefault().Id;
                    BookAuthor bookAuthor = new BookAuthor() { AuthorId = authorid, BookId = id };
                    _bookauthor.Insert(bookAuthor);
                    _bookauthor.Save();
                }
                foreach (var category in categories)
                {
                    var categoryid = _category.AllQuery.Where(x => x.Title == category).FirstOrDefault().Id;
                    BookCategory bookCategory = new BookCategory() { CategoryId = categoryid, BookId = id };
                    _bookcategory.Insert(bookCategory);
                    _bookauthor.Save();
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<myOption> GetFiltersBook(FilterOption filterOption)
        {
            List<Book> books = new List<Book>();
            List<int> booksids = new List<int>();
            List<myOption> options = new List<myOption>();


            if (filterOption.authors.Count != 0)
            {
                foreach (var a in filterOption.authors)
                {
                    foreach (var b in _bookauthor.AllQuery.ToList())
                    {
                        if (b.AuthorId == a)
                        {
                            booksids.Add(b.BookId);
                        }
                    }
                }
                
                booksids.Distinct();
                foreach (var id in booksids)
                {
                    books.AddRange(_book.AllQuery.ToList().Where(x => x.Id == id));
                }
               

            }
            ///////////////////////////////////////////////////////////////////////////////////////////
            
            ///////////////////////////////////////////////////////////////////////////////////////////
            if (filterOption.categories.Count != 0)
            {
                
                booksids = new List<int>();
                foreach (var a in filterOption.categories)
                {
                    foreach (var b in _bookcategory.AllQuery.ToList())
                    {
                        if (b.CategoryId == a)
                        {
                            booksids.Add(b.BookId);
                        }
                    }
                }
                


                List<Book> newbox = new List<Book>();
                if (books.Count != 0)
                {
                    foreach (var id in booksids)
                    {
                        newbox.AddRange(books.ToList().Where(x => x.Id == id));
                    }
                    books.Clear(); 
                    books = newbox;
                }
                else
                {
                    foreach (var id in booksids)
                    {
                        books.AddRange(_book.AllQuery.ToList().Where(x => x.Id == id));
                    }
                }

                
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            if (filterOption.languages.Count != 0)
            {
                if (books.Count == 0)
                    books = _book.AllQuery.ToList();

                booksids = new List<int>();
                foreach (var a in filterOption.languages)
                {
                    foreach (var b in books)
                    {
                        if (b.LanguageId == a)
                        {
                            booksids.Add(b.Id);
                        }
                    }
                }
                if (booksids.Count==0)
                {
                    books.Clear();
                }
                else
                {
                    List<Book> newboox = new List<Book>();
                    foreach (var id in booksids)
                    {
                        newboox.AddRange(books.ToList().Where(x => x.Id == id));
                    }
                    books.Clear();books = newboox;
                }
                
            }
            if (filterOption.search != "" && filterOption.search !=null)
            {   
                books.AddRange(_book.AllQuery.ToList().Where(x => x.Title.ToUpper().Contains(filterOption.search.ToUpper())));
            }
            books.Distinct();
            Int32 index = books.Count - 1;
            while (index > 0)
            {
                if (books[index].Id == books[index - 1].Id)
                {
                    if (index < books.Count - 1)
                        (books[index].Id, books[books.Count - 1].Id) = (books[books.Count - 1].Id, books[index].Id);
                    books.RemoveAt(books.Count - 1);
                    index--;
                }
                else
                    index--;
            }


            foreach (var item in books)
            {
                options.Add(new myOption()
                {
                    Id = item.Id,
                    Description = item.Description,
                    Title = item.Title,
                    Price = item.Price,
                    Imagename = item.Imagename,
                    Edition = item.Edition,
                    LanguageId = item.LanguageId,
                    UserId = item.UserId,
                    AgeRestriction=item.AgeRestriction,
                    ReleaseDate=item.ReleaseDate
                });
            }
            return options;
        }
        public class myOption
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }
            public decimal Price { get; set; }
            public string Imagename { get; set; }
            public int? Edition { get; set; } = 1;

            public int? AgeRestriction { get; set; }

            public DateTime? ReleaseDate { get; set; }

            public int LanguageId { get; set; }
            public int? UserId { get; set; }

        }
    }


}
