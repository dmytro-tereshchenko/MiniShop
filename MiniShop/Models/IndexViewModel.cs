using MiniShop.Models.Bd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniShop.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Good> Goods { get; set; }
        public PageInfo PageInfo { get; set; }
        public string SelectedCategory { get; set; }
        public string SearchTemplate { get; set; }

    }
}