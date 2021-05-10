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
    public class CategoryController : ControllerBase
    {
        private readonly IStringLocalizer<AuthController> _localizer;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public CategoryController(IMapper mapper, IStringLocalizer<AuthController> localizer, ICategoryService categoryService)
        {
            _mapper = mapper;
            _localizer = localizer;
            _categoryService = categoryService;
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("category-detail/{id:int}")]
        public ActionResult DetailCategory(int id)
        {
            return Ok(_categoryService.GetCategoryById(id));
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("category-list")]
        public ActionResult getCategories(int id)
        {
            return Ok(_categoryService.GetCategories());
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("delete-category/{id:int}")]
        public ActionResult deleteCategory(int id)
        {
            try
            {
                SimpleResponse sr = new SimpleResponse();
                int errorCode = 0;
                string result = "";
                _categoryService.DeleteCategoryByIdAsync(id, ref errorCode, ref result);

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
        [HttpPost, Route("edit-category/{id:int}")]
        public ActionResult editCategory(int id, [FromBody] Category category)
        {
            SimpleResponse sr = new SimpleResponse();
            if (ModelState.IsValid)
            {
                var categorydb = _categoryService.GetCategoryById(id);
                if (categorydb == null) return NotFound();



                int errorCode = 0;
                string result = "";

                _categoryService.UpdateCategory(id, category, ref errorCode, ref result);
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
        [HttpPost, Route("add-category")]
        public ActionResult addCategory([FromBody] Category category)
        {
            SimpleResponse sr = new SimpleResponse();
            int errorCode = 0;
            string result = "";
            _categoryService.CreateCategory(category, ref errorCode, ref result);
            sr.ErrorCode = errorCode;
            sr.Result = result;
            return Ok(sr);
        }
    }
}