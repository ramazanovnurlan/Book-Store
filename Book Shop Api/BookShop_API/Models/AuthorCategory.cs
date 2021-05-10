using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.Models
{
    public class AuthorCategory
    {
        public List<string> BookAuthor{ get; set; }
        public List<string> BookCategory { get; set; }
        public int authorcount { get; set; }
        public int categorycount { get; set; }
    }
}
