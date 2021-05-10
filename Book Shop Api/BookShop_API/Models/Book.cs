using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Imagename { get; set; }
        public int? Edition { get; set; } = 1;

        public int? AgeRestriction { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        public int LanguageId { get; set; }

        public Language Language { get; set; }

        public List<BookCategory> BookCategories { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }
        public int? UserId { get; set; }
        public virtual AppUser User { get; set; }


        public Book()
        {
            this.BookCategories = new List<BookCategory>();

            this.BookAuthors = new List<BookAuthor>();

        }
    }
}
