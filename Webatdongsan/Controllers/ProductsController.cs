using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using Webatdongsan.Models;
using System.Net;

namespace Webatdongsan.Controllers
{
    public class ProductsController : Controller
    {
        private WebbatdongsanEntities db = new WebbatdongsanEntities();
        // GET: Products
        public ActionResult Index()
        {
            Customer khachhang = (Customer)Session["Taikhoan"];

            if (khachhang != null)
            {
                ViewBag.UserID = khachhang.IdNguoiDung;
            }
                var products = db.Products.Include(p => p.Category1);
            return View(products.ToList());
        }
        public ActionResult ADView()
        {
            var products = db.Products.Include(p => p.Category1);
            return View(products.ToList());
        }
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.Category, "IDCate", "NameCate");
            return View();
        }
        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,NamePro,DecriptionPro,Category,Price,ImagePro,Area,Address,Phone,LienHe,NgayDang,ImagePro1,ImagePro2")] Products product, HttpPostedFileBase ImagePro, HttpPostedFileBase ImagePro1, HttpPostedFileBase ImagePro2)
        {
            if (ModelState.IsValid)
            {
                Customer khachhang = (Customer)Session["Taikhoan"];

                if (khachhang != null)
                {
                    product.IdNguoiDung = khachhang.IdNguoiDung;
                    
                    if (ImagePro != null)
                    {
                        var fileName = Path.GetFileName(ImagePro.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                        product.ImagePro = fileName;
                        ImagePro.SaveAs(path);
                    }

                    if (ImagePro1 != null)
                    {
                        var fileName1 = Path.GetFileName(ImagePro1.FileName);
                        var path1 = Path.Combine(Server.MapPath("~/Images"), fileName1);
                        product.ImagePro1 = fileName1;
                        ImagePro1.SaveAs(path1);
                    }

                    if (ImagePro2 != null)
                    {
                        var fileName2 = Path.GetFileName(ImagePro2.FileName);
                        var path2 = Path.Combine(Server.MapPath("~/Images"), fileName2);
                        product.ImagePro2 = fileName2;
                        ImagePro2.SaveAs(path2);
                    }

                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    // Xử lý khi khachhang là null (không đăng nhập)
                    return RedirectToAction("Login", "Account"); // Hoặc chuyển hướng tới trang đăng nhập
                }
            }

            ViewBag.Category = new SelectList(db.Category, "IDCate", "NameCate", product.Category);
            return View(product);
        }


        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = new SelectList(db.Category, "IDCate", "NameCate", product.Category);
            return View(product);
        }
        public ActionResult Addwl(int id)
        {
            Products products = db.Products.Find(id);
            products.WhiteList = 1;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,NamePro,DecriptionPro,Category,Price,ImagePro,Area,Address,Phone,LienHe,NgayDang")] Products product, HttpPostedFileBase ImagePro)
        {
            if (ModelState.IsValid)
            {
                var productDB = db.Products.FirstOrDefault(p => p.ProductID ==
                product.ProductID);
                if (productDB != null)
                {
                    productDB.NamePro = product.NamePro;

                    productDB.DecriptionPro = product.DecriptionPro;
                    productDB.Price = product.Price;
                    if (ImagePro != null)

                    {
                        //Lấy tên file của hình được up lên
                        var fileName = Path.GetFileName(ImagePro.FileName);

                        //Tạo đường dẫn tới file

                        var path = Path.Combine(Server.MapPath("~/Images"),
                        fileName);
                        //Lưu tên

                        productDB.ImagePro = fileName;
                        //Save vào Images Folder
                        ImagePro.SaveAs(path);

                    }

                    productDB.Category = product.Category;

                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category = new SelectList(db.Category, "IDCate", "NameCate",
            product.Category);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}