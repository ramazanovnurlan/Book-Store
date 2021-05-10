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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BookShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IStringLocalizer<AuthController> _localizer;
        private readonly IMapper _mapper;
        private readonly IUserService _usernService;
        public UserController(IMapper mapper, IStringLocalizer<AuthController> localizer, IUserService userService)
        {
            _mapper = mapper;
            _localizer = localizer;
            _usernService = userService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost, Route("edit-user-info/{id:int}")]
        public IActionResult EditUserInfo(int id, [FromBody] UserInfo model)
        {
            SimpleResponse sr = new SimpleResponse();
            if (ModelState.IsValid)
            {
                var userDb = _usernService.getUserById(id);
                if (userDb == null) return NotFound();

                var passwordHasher = new PasswordHasher<AppUser>();
                var hashedPassword = passwordHasher.HashPassword(userDb, model.Password);

                int errorCode = 0;
                string result = "";
                userDb.Email = model.Email;
                userDb.PasswordHash = hashedPassword;
                userDb.UserName = model.Username;
                userDb.Name = model.Name;
                userDb.Surname = model.Surname;
                _usernService.EditUser(userDb, ref errorCode, ref result);
                sr.ErrorCode = errorCode;
                sr.Result = result;

                
                return Ok(sr);

            }

            else
            {
                sr.ErrorCode = 1;
                sr.Result = "Silmek mumkun olmadi";
                return Ok(sr);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("get-user-info/{id:int}")]
        public ActionResult<AppUser> GetUserInfo(int id)
        {
            var userDb = _usernService.getUserById(id);
            if (userDb == null) return NotFound();

            var user = _usernService.getUserById(userDb.Id);

            return Ok(user);

        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost, Route("delete-user/{id:int}")]
        public IActionResult DeleteUserInfo(int id, [FromBody] AppUser model)
        {
            SimpleResponse sr = new SimpleResponse();
            var userDb = _usernService.getUserById(id);
            if (userDb == null) return NotFound();

            int errorCode = 0;
            string result = "";
            _usernService.DeleteUser(userDb, ref errorCode, ref result);
            sr.ErrorCode = errorCode;
            sr.Result = result;
            return Ok(sr);

        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("get-users")]
        public ActionResult<List<AppUser>> GetUsers()
        {
            var currentUser = HttpContext.User;
            
            return Ok(_usernService.getUsers());

        }
    }
}