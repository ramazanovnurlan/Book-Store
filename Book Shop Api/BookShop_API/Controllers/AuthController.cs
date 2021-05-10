using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookShop_API.BookShopServices.Helper;
using BookShop_API.BookShopServices.Interface;
using BookShop_API.Helper;
using BookShop_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BookShop_API.Controllers
{
    //[Route("api/[controller]")]
   //[ApiController]
    [Route("{culture:culture}/api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IStringLocalizer<AuthController> _localizer;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthController(IMapper mapper, IAuthService authService, IStringLocalizer<AuthController> localizer)
        {
            _mapper = mapper;
            _authService = authService;
            _localizer = localizer;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] Loginmodel model)
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            model.Lang = rqf.RequestCulture.Culture.ToString().Substring(0, 2);
            LoginResponse r = new LoginResponse();
            if (ModelState.IsValid)
            {
                r = await _authService.Login(model.Email, model.Password, model.Lang);
                return Ok(r);
            }
            else
            {
                r.resultSituation = new SimpleResponse();
                r.resultSituation.ErrorCode = 1;
                r.resultSituation.Result = _localizer["ModelInValid"];
                return Ok(r);
            }


        }


        [HttpPost, Route("register")]
        public IActionResult Checkin([FromBody] Checkin model)
        {
            SimpleResponse sr = new SimpleResponse();
            if (ModelState.IsValid)
            {
                AppUser p = _mapper.Map<Checkin, AppUser>(model);
                int errorCode = 0;
                string result = "";
                _authService.Register(p, model.Password, ref errorCode, ref result);
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
        [HttpPost, Route("edit")]
        public IActionResult Edit([FromBody] Checkin model)
        {
            return Ok();
        }
    }
}