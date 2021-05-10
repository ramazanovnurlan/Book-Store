using BookShop_API.BookShopServices.Helper;
using BookShop_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.BookShopServices.Interface
{
    public interface IAuthService
    {
        void Register(AppUser p, string password, ref int errorCode, ref string result);

        Task<LoginResponse> Login(string email, string password, string lang);
    }
}
