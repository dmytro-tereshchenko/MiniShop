using MiniShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniShop.Controllers
{
    public class NavController : Controller
    {
        private IUnitOfWork unitOfWork;
        public NavController(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;
        public PartialViewResult Menu(string category = null)
        {
            IEnumerable<string> categories = unitOfWork.Categories.GetAll().Select(c => c.Name);
            return PartialView(categories);
        }

    }
}