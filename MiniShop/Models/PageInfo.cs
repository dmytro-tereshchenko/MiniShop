﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniShop.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages // всего страниц
        {
            get => (int)Math.Ceiling((decimal)TotalItems / PageSize);
        }
    }
}