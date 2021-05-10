using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.Helper
{
    public class BookModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Imagename { get; set; }
        public int? Edition { get; set; } = 1;
        public int? AgeRestriction { get; set; }
        public string Languagename { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? UserId { get; set; }
        public string author { get; set; }
        public string category { get; set; }
        public List<string> authors { get; set; }
        public List<string> categories { get; set; }

    }
}
