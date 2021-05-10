using BookShop_API.BookShop_Repostory.Context;
using BookShop_API.BookShop_Repostory.Repostory;
using BookShop_API.BookShopServices.AppConfig;
using BookShop_API.BookShopServices.Interface;
using BookShop_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.BookShopServices.Implementation
{
    public class UserService:IUserService
    {
        AppConfiguration config = new AppConfiguration();
        private readonly IRepository<AppUser> _person;
        private readonly IRepository<Errors> _errors;
        //    private readonly ILoggerManager _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IdentityAppContext _context;

        public UserService(IRepository<AppUser> person,
            IRepository<Errors> errors, UserManager<AppUser> userManager, IdentityAppContext context
            )
        {
            _person = person;
            _errors = errors;
            _userManager = userManager;
            _context = context;
        }
        public List<AppUser> getUsers()
        {
            return _userManager.Users.ToList();
        }
        
        public AppUser getUserById(int id)
        {
            return _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public void EditUser(AppUser appUser, ref int errorCode, ref string result)
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id == appUser.Id);
                user.UserName = appUser.UserName;
                user.Name = appUser.Name;
                user.Surname = appUser.Surname;
                user.AccessFailedCount = appUser.AccessFailedCount;
                user.EmailConfirmed = appUser.EmailConfirmed;
                user.Email = appUser.Email;
                user.LockoutEnabled = appUser.LockoutEnabled;
                user.LockoutEnd = appUser.LockoutEnd;
                user.NormalizedEmail = appUser.NormalizedEmail;
                user.NormalizedUserName = appUser.NormalizedUserName;
                user.PasswordHash = appUser.PasswordHash;
                user.PhoneNumber = appUser.PhoneNumber;
                user.PhoneNumberConfirmed = appUser.PhoneNumberConfirmed;
                user.SecurityStamp = appUser.SecurityStamp;
                user.ConcurrencyStamp = appUser.ConcurrencyStamp;
                //user.TwoFactorEnabled = appUser.TwoFactorEnabled;
                _userManager.UpdateAsync(user).GetAwaiter().GetResult();
                _context.SaveChanges();
                errorCode = 0;
                result = "Uğurla başa çatdı!";

            }
            catch (Exception e)
            {
                errorCode = 1;
                result = "Məlumatın sistemə əlavə olunması baş tutmadı!";
            }
        }

        public void DeleteUser(AppUser p, ref int errorCode, ref string result)
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id == p.Id);
                _userManager.DeleteAsync(user).GetAwaiter().GetResult();
                _context.SaveChanges();
                errorCode = 0;
                result = "Uğurla başa çatdı!";

            }
            catch (Exception e)
            {
                errorCode = 1;
                result = "Məlumatın sistemə əlavə olunması baş tutmadı!";
            }
        }

        
    }
}
