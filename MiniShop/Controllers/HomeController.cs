using MiniShop.Interfaces;
using MiniShop.Models;
using MiniShop.Models.Bd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MiniShop.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork;
        public HomeController(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;
        public ActionResult Index(string category, int page = 1)
        {
            int pageSize = 3;
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = category == null ? unitOfWork.Goods.Count :
                unitOfWork.Goods.GetAll().Where(g => g.Category.Name == category).Count()

            };
            IEnumerable<Good> goods = category == null ? unitOfWork.Goods.GetAll().
                OrderBy(g => g.Id).
                Skip((page - 1) * pageSize).Take(pageSize)
                : unitOfWork.Goods.GetAll().
                Where(g => g.Category.Name == category).
                OrderBy(g => g.Id).
                Skip((page - 1) * pageSize).Take(pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                Goods = goods,
                PageInfo = pageInfo,
                SelectedCategory = category
            };
            return View(viewModel);
        }
        public async Task<ActionResult> ShowGood(int id)
        {
            Good good = await unitOfWork.Goods.GetById(id);
            if (good != null)
            {
                return View(good);
            }
            else
            {
                return HttpNotFound();
            }
        }
        public async Task<ActionResult> SearchGood(string goodName)
        {
            /*IEnumerable<Good> goods = await unitOfWork.Goods.GetAllByName(goodName);
            if (goods != null)
            {
                int pageSize = 3;
                PageInfo pageInfo = new PageInfo
                {
                    PageNumber = 1,
                    PageSize = pageSize,
                    TotalItems = goods.Count()

                };
                IndexViewModel viewModel = new IndexViewModel
                {
                    Goods = goods,
                    PageInfo = pageInfo,
                    SelectedCategory = null
                };
                return View("Index", viewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }*/
            Good good = await unitOfWork.Goods.GetByName(goodName);
            if (good != null)
            {
                return View("ShowGood", good);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}