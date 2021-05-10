using BookShop_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.BookShopServices.Interface
{
    public interface IUserService
    {
        AppUser getUserById(int id);

        List<AppUser> getUsers();
        void EditUser(AppUser p, ref int errorCode, ref string result);
        void DeleteUser(AppUser p, ref int errorCode, ref string result);
    }
}
