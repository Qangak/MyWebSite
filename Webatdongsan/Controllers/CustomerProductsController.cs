using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webatdongsan.Models;

namespace Webatdongsan.Controllers
{
    public class CustomerProductsController : Controller
    {
        private WebbatdongsanEntities db = new WebbatdongsanEntities();
        // GET: CustomerProducts
        public ActionResult Index(string SearchString = "")
        {
            Customer khachhang = (Customer)Session["Taikhoan"];
            
            if (SearchString != "")
            {
                var product = db.Products.Where(x => x.NamePro.ToUpper().Contains(SearchString.ToUpper())|| x.Address.ToUpper().Contains(SearchString.ToUpper()));
                              //db.Products.Where(x => x.Address.ToUpper().Contains(SearchString.ToUpper()));
                
                return View(product.ToList());
            }
            var products = db.Products;
            return View(products.ToList());
        }

        public ActionResult Chuyen()
        {
            Customer khachhang = (Customer)Session["Taikhoan"];
            if (khachhang != null)
            {
                return RedirectToAction("Create", "Products", new { idNguoiDung = khachhang.IdNguoiDung });
            }
            else
            {
                return RedirectToAction("Login", "Account"); // Hoặc chuyển hướng tới trang đăng nhập
            }
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new
                HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult GetProductsByCategory()
        {
            var categories = db.Category.ToList();
            return PartialView("CategoriesPartialView", categories);
        }
        public ActionResult GetProductsByCateId(int id)
        {
            var products = db.Products.Where(p => p.Category1.Id ==
            id).ToList();
            return View("Index", products);
        }
    }   
}