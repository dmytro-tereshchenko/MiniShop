using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiniShop.Models.Bd;
using MiniShop.Interfaces;
using MiniShop.Models;

namespace MiniShop.Controllers
{
    public class GoodsController : Controller
    {
        private IUnitOfWork unitOfWork;
        public GoodsController(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;
        public async Task<ActionResult> Index(string category, int page = 1, string searchTemplate = null)
        {
            int pageSize = 3;
            IEnumerable<Good> goods = searchTemplate == null ? unitOfWork.Goods.GetAll() : await unitOfWork.Goods.GetAllByName(searchTemplate);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = category == null ? goods.Count() :
                goods.Where(g => g.Category.Name == category).Count()

            };
            IEnumerable<Good> goodsResult = category == null ? goods.
                OrderBy(g => g.Id).
                Skip((page - 1) * pageSize).Take(pageSize)
                : goods.
                Where(g => g.Category.Name == category).
                OrderBy(g => g.Id).
                Skip((page - 1) * pageSize).Take(pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                Goods = goodsResult,
                PageInfo = pageInfo,
                SelectedCategory = category,
                SearchTemplate = searchTemplate
            };
            return View(viewModel);
        }

        public ActionResult CreateGood()
        {
            ViewBag.CategoryId = new SelectList(unitOfWork.Categories.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateGood([Bind(Include = "Id,Name,Description,Price,CategoryId")] Good good)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.Goods.Add(good);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(unitOfWork .Categories.GetAll(), "Id", "Name", good.CategoryId);
            return View(good);
        }
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory([Bind(Include = "Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.Categories.Add(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Good good = await unitOfWork.Goods.GetById(id.Value);
            if (good == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(unitOfWork .Categories.GetAll(), "Id", "Name", good.CategoryId);
            return View(good);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Price,CategoryId")] Good good)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.Goods.Update(good);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(unitOfWork.Categories.GetAll(), "Id", "Name", good.CategoryId);
            return View(good);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Good good = await unitOfWork.Goods.GetById(id.Value);
            if (good == null)
            {
                return HttpNotFound();
            }
            return View(good);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await unitOfWork.Goods.Delete(id);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> SearchGood(string goodName)
        {
            IEnumerable<Good> goods = await unitOfWork.Goods.GetAllByName(goodName);
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
                    SelectedCategory = null,
                    SearchTemplate = goodName
                };
                return View("Index", viewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
            /*Good good = await unitOfWork.Goods.GetByName(goodName);
            if (good != null)
            {
                return View("ShowGood", good);
            }
            else
            {
                return RedirectToAction("Index");
            }*/
        }
    }
}
