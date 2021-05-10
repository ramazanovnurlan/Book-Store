using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.Models
{
    public partial class Errors
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("KEY")]
        public string Key { get; set; }

        [Column("ERROR_AZ")]
        public string ErrorAZ { get; set; }

        [Column("ERROR_EN")]
        public string ErrorEN { get; set; }
        [Column("ERROR_RU")]
        public string ErrorRU { get; set; }

    }
}
