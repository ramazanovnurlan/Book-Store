using BookShop_API.BookShop_Repostory.Repostory;
using BookShop_API.BookShopServices.AppConfig;
using BookShop_API.BookShopServices.Helper;
using BookShop_API.BookShopServices.Helper.Enums;
using BookShop_API.BookShopServices.Interface;
using BookShop_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace BookShop_API.BookShopServices.Implementation
{
    public class AuthService : IAuthService
    {
        AppConfiguration config = new AppConfiguration();
        private readonly IRepository<AppUser> _persons;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IRepository<Errors> _errors;

        public AuthService(IRepository<AppUser> persons, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IRepository<Errors> errors
            )
        {
            _persons = persons;
            _userManager = userManager;
            _signInManager = signInManager;
            _errors = errors;
        }
        public async Task<LoginResponse> Login(string email, string password, string lang)
        {
            LoginResponse r = new LoginResponse();
            r.resultSituation = new SimpleResponse();
            try
            {
                AppUser user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    r.resultSituation.ErrorCode = 1;
                    var obj = _errors.AllQuery.FirstOrDefault(x => x.Key == ERRORS.PERSON_NOT_FOUND.ToString());
                    r.resultSituation.Result = (string)obj.GetType().GetProperty("Error" + lang.ToUpper()).GetValue(obj, null);

                    return r;
                }
                else
                {
                    var res_check = await _userManager.CheckPasswordAsync(user, password);
                    if (res_check)
                    {
                        if (!(await _signInManager.CanSignInAsync(user)))
                        {
                            r.resultSituation.ErrorCode = 1;
                            var obj = _errors.AllQuery.FirstOrDefault(x => x.Key == ERRORS.NOT_ACCESS.ToString());
                            r.resultSituation.Result = (string)obj.GetType().GetProperty("Error" + lang.ToUpper()).GetValue(obj, null);
                            return r;
                        }
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        r.jwt = GenerateToken(user);
                        r.resultSituation.ErrorCode = 0;

                    }
                    else
                    {
                        r.resultSituation.ErrorCode = 1;
                        var obj = _errors.AllQuery.FirstOrDefault(x => x.Key == ERRORS.PASSWORD_INCORRECT.ToString());
                        r.resultSituation.Result = (string)obj.GetType().GetProperty("Error" + lang.ToUpper()).GetValue(obj, null);

                    }
                    return r;
                }
            }
            catch (Exception e)
            {
                r.resultSituation.ErrorCode = 1;
                r.resultSituation.Result = "Giriş prosesi baş tutmadı!";
            }
            return r;
        }

        public void Register(AppUser p, string password, ref int errorCode, ref string result)
        {
            try
            {
                
                if (_persons.FindBy(x => x.UserName == p.UserName).Count() > 0)
                {
                    errorCode = 1;
                    result = "Bu istfadəçi adı sistemdə artıq var!";
                    return;
                }
                else if (_persons.FindBy(x => x.Email == p.Email).Count() > 0)
                {
                    errorCode = 1;
                    result = "Sistemdə artıq bu email var!";
                    return;
                }
                else
                {
                    var register = _userManager.CreateAsync(p, password);
                    if (register.Result.Succeeded)
                    {
                        errorCode = 0;
                        result = "Qeydiyyat uğurla başa çatdı!";
                    }
                    else
                    {
                        errorCode = 1;
                        result = "Qeydiyyat zamanı xəta başa verdi!";

                    }
                }
            }
            catch (Exception e)
            {
                // _logger.LogError(e.Message);
                errorCode = 1;
                result = "Məlumatın sistemə əlavə olunması baş tutmadı!";
            }
        }

        private JwtResponse GenerateToken(AppUser user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim("UserName", user.Name.ToString() + " " + user.Surname.ToString()),
                        new Claim("UserEmail", user.Email.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JwtSecret)), SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            var expiration = DateTime.UtcNow.AddDays(7);
            return new JwtResponse(token, expiration);
        }
    }
}
