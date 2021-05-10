using System;

namespace BookShop_API.BookShopServices.Helper
{
    public class JwtResponse
    {
        public JwtResponse(string token, DateTime expireAt)
        {
            Token = token;
            ExpireAt = expireAt;
        }

        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}