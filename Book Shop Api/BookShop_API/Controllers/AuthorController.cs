using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookShop_API.BookShopServices.Helper;
using BookShop_API.BookShopServices.Interface;
using BookShop_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BookShop_API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Route("{culture:culture}/api/v1/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IStringLocalizer<AuthController> _localizer;
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;
        public AuthorController(IMapper mapper, IStringLocalizer<AuthController> localizer, IAuthorService authorService)
        {
            _mapper = mapper;
            _localizer = localizer;
            _authorService = authorService;
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("author-detail/{id:int}")]
        public ActionResult DetailAuthor(int id)
        {
           return Ok( _authorService.GetAuthorById(id));
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("author-list")]
        public ActionResult getAuthors(int id)
        {
            return Ok(_authorService.GetAuthors());
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("delete-author/{id:int}")]
        public ActionResult deleteAuthor(int id)
        {
            try
            {
                SimpleResponse sr = new SimpleResponse();
                int errorCode = 0;
                string result = "";
                _authorService.DeleteAuthorByIdAsync(id,ref errorCode,ref result);

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
        [HttpPost, Route("edit-author/{id:int}")]
        public ActionResult editAuthor(int id,[FromBody] Author author)
        {
            SimpleResponse sr = new SimpleResponse();
            if (ModelState.IsValid)
            {
                var authordb = _authorService.GetAuthorById(id);
                if (authordb == null) return NotFound();

               

                int errorCode = 0;
                string result = "";
                
                _authorService.UpdateAuthor(id, author, ref errorCode, ref result);
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
        [HttpPost, Route("add-author")]
        public ActionResult addAuthor([FromBody] Author author)
        {
            SimpleResponse sr = new SimpleResponse();
            int errorCode = 0;
            string result = "";
            _authorService.CreateAuthor(author,ref errorCode,ref result);
            sr.ErrorCode = errorCode;
            sr.Result = result;
            return Ok(sr);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("getauthorcategory/{id:int}")]
        public ActionResult getAuthorCategory(int id)
        {
            var a = _authorService.GetAuthorCategory(id);
            return Ok(a);
        }

    }
}