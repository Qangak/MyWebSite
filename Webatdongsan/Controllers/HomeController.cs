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
    
    public class HomeController : Controller
    {
        private WebbatdongsanEntities db = new WebbatdongsanEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult home(string SearchString = "")
        {
            if (SearchString != "")
            {
                var product = db.Products.Where(x => x.NamePro.ToUpper().Contains(SearchString.ToUpper()));
                db.Products.Where(x => x.Address.ToUpper().Contains(SearchString.ToUpper()));

                return View(product.ToList());
            }
            var products = db.Products;
            return View(products.ToList());
        }
    }
}