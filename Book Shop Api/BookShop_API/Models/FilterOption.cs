using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.Models
{
    public class FilterOption
    {
        public List<int> authors;
        public List<int> categories;
        public List<int> languages;
        public string search;
    }
}
