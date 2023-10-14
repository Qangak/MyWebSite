using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webatdongsan.Models;

namespace Webatdongsan.Controllers
{
    public class CategoriesController : Controller
    {
        private WebbatdongsanEntities datebase = new WebbatdongsanEntities();
        // GET: Categories
        public ActionResult Index()
        {
            var categories = datebase.Category.ToList();
            return View(categories);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                datebase.Category.Add(category);
                datebase.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi tạo mới Category");
            }
        }
        public ActionResult Details(int id)
        {
            var category = datebase.Category.Where(c => c.Id ==
            id).FirstOrDefault();
            return View(category);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = datebase.Category.Where(c => c.Id ==
            id).FirstOrDefault();
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {
            datebase.Entry(category).State =
            System.Data.Entity.EntityState.Modified;
            datebase.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new
                HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = datebase.Category.Where(c => c.Id ==
            id).FirstOrDefault();
            if (category == null)

            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var category = datebase.Category.Where(c => c.Id ==
                id).FirstOrDefault();
                datebase.Category.Remove(category);
                datebase.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác");
            }
        }
    }
}