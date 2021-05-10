using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.Helper
{
    public class Checkin
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Lang { get; set; }
    }
}
