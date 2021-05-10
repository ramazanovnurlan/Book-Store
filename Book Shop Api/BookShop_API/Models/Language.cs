﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.Models
{
    public class Language
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<Book> Books { get; set; }

        public Language()
        {
            this.Books = new List<Book>();
        }
    }
}
