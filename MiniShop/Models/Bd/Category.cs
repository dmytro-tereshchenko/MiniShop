using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniShop.Models.Bd
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required, DisplayName("Category")]
        public string Name { get; set; }
        public virtual ICollection<Good> Goods { get; set; } = new List<Good>();
    }
}