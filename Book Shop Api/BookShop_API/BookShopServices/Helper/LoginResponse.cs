using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.BookShopServices.Helper
{
    public class LoginResponse
    {
        public JwtResponse jwt { get; set; }
        public SimpleResponse resultSituation { get; set; }
    }
}
