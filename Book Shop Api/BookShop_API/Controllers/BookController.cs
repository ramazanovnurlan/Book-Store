using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookShop_API.BookShopServices.Helper;
using BookShop_API.BookShopServices.Interface;
using BookShop_API.Helper;
using BookShop_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using static BookShop_API.BookShopServices.Implementation.BookService;

namespace BookShop_API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Route("{culture:culture}/api/v1/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IStringLocalizer<AuthController> _localizer;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        public BookController(IMapper mapper, IStringLocalizer<AuthController> localizer, IBookService bookService)
        {
            _mapper = mapper;
            _localizer = localizer;
            _bookService = bookService;
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost, Route("new-book")]
        public IActionResult NewBook([FromBody] BookModel model)
        {
            var currentUser = HttpContext.User;
            int currentUserId = Convert.ToInt32(currentUser.FindFirst("UserId").Value);
            SimpleResponse sr = new SimpleResponse();
            if (ModelState.IsValid)
            {
                model.UserId = currentUserId;
                Book book = _mapper.Map<BookModel, Book>(model);
                int errorCode = 0;
                string result = "";
                _bookService.CreateBook(book, model.Languagename, model.authors, model.categories, ref errorCode, ref result);
                sr.ErrorCode = errorCode;
                sr.Result = result;
                return Ok(sr);
            }
            else
            {
                sr.ErrorCode = 1;
                sr.Result = _localizer["ModelStructure"];
                return Ok(sr);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost, Route("edit-book/{id:int}")]
        public IActionResult EditCard(int id, [FromBody] BookModel model)
        {
            SimpleResponse sr = new SimpleResponse();
            if (ModelState.IsValid)
            {
                Book book = _mapper.Map<BookModel, Book>(model);
                int errorCode = 0;
                string result = "";
                _bookService.UpdateBook(id, book, model.Languagename, model.authors,model.categories, ref errorCode, ref result);
                sr.ErrorCode = errorCode;
                sr.Result = result;
                return Ok(sr);
            }
            else
            {
                sr.ErrorCode = 1;
                sr.Result = _localizer["ModelStructure"];
                return Ok(sr);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("userbook-list")]
        public IActionResult GetUserBookList()
        {
            var currentUser = HttpContext.User;
            int currentUserId = Convert.ToInt32(currentUser.FindFirst("UserID").Value);
            return Ok(_bookService.GetUserBooks(currentUserId));
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("book-list")]
        public IActionResult GetBookList()
        {
            return Ok(_bookService.GetBooks().OrderByDescending(x=>x.Id));
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("count")]
        public IActionResult Count()
        {
            return Ok(_bookService.GetBooks().Count);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("book-detail/{id:int}")]
        public IActionResult DetailBook(int id)
        {
            var currentUser = HttpContext.User;
            int currentUserId = Convert.ToInt32(currentUser.FindFirst("UserID").Value);
            return Ok(_bookService.GetBookById(id, currentUserId));
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("book-delete/{id:int}")]
        public ActionResult DeleteBook(int id)
        {
            try
            {
                SimpleResponse sr = new SimpleResponse();
                int errorCode = 0;
                string result = "";
                _bookService.DeleteBookById(id, ref errorCode, ref result);

                sr.ErrorCode = errorCode;
                sr.Result = result;


                return Ok(sr);
            }
            catch (Exception)
            {
                SimpleResponse sr = new SimpleResponse();
                sr.ErrorCode = 1;
                sr.Result = "Silmek mumkun olmadi";
                return Ok(sr);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost, Route("book-filterlist")]
        public IActionResult GetFilterBookList([FromBody]FilterOption filterOption)
        {
            List<myOption> books = _bookService.GetFiltersBook(filterOption);

            return Ok(books);
        }
    }
}