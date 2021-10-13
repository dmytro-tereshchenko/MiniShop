using MiniShop.Interfaces;
using MiniShop.Models.Bd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MiniShop.Controllers
{
    public class NavController : Controller
    {
        private IUnitOfWork unitOfWork;
        public NavController(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;
        public PartialViewResult Menu(string control = "Home", string category = null)
        {
            IEnumerable<Category> categories = unitOfWork.Categories.GetAll();
            ViewBag.Control = control;
            return PartialView(categories.Select(c => c.Name));
        }
    }
}