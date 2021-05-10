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
    public class LanguageController : ControllerBase
    {
        private readonly IStringLocalizer<AuthController> _localizer;
        private readonly IMapper _mapper;
        private readonly ILanguageService _languageService;
        public LanguageController(IMapper mapper, IStringLocalizer<AuthController> localizer, ILanguageService languageService)
        {
            _mapper = mapper;
            _localizer = localizer;
            _languageService = languageService;
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("language-detail/{id:int}")]
        public ActionResult DetailLanguage(int id)
        {
            return Ok(_languageService.GetLanguageById(id));
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("language-list")]
        public ActionResult getLanguages(int id)
        {
            return Ok(_languageService.GetLanguages());
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("delete-language/{id:int}")]
        public ActionResult deleteLanguage(int id)
        {
            try
            {
                SimpleResponse sr = new SimpleResponse();
                int errorCode = 0;
                string result = "";
                _languageService.DeleteLanguageById(id, ref errorCode, ref result);

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
        [HttpPost, Route("edit-language/{id:int}")]
        public ActionResult editLanguage(int id, [FromBody] Language language)
        {
            SimpleResponse sr = new SimpleResponse();
            if (ModelState.IsValid)
            {
                var languagedb = _languageService.GetLanguageById(id);
                if (languagedb == null) return NotFound();



                int errorCode = 0;
                string result = "";

                _languageService.UpdateLanguage(id,language , ref errorCode, ref result);
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
        [HttpPost, Route("add-language")]
        public ActionResult addLanguage([FromBody] Language language)
        {
            SimpleResponse sr = new SimpleResponse();
            int errorCode = 0;
            string result = "";
            _languageService.CreateLanguage(language, ref errorCode, ref result);
            sr.ErrorCode = errorCode;
            sr.Result = result;
            return Ok(sr);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("langStr/{id:int}")]
        public ActionResult GetLangstr(int id)
        {
            return Ok(_languageService.GetLangStr(id));
        }
    }
}