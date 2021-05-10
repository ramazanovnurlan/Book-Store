using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.Models
{
    public partial class AppUser : IdentityUser<int>
    {

        [Column("NAME")]
        public string Name { get; set; }
        [Column("SURNAME")]
        public string Surname { get; set; }
        [Column("BIRTH_DATE")]
        public DateTime? BirthDate { get; set; }
     

        [NotMapped]
        public string FullName
        {
            get { return Name + " " + Surname; }
        }


    }
}
