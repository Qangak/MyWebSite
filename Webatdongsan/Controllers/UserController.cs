using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webatdongsan.Models;

namespace Webatdongsan.Controllers
{
    public class UserController : Controller
    {
        private WebbatdongsanEntities database = new WebbatdongsanEntities();
        // GET: Users
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer cust)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cust.username))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(cust.password))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (string.IsNullOrEmpty(cust.email))
                    ModelState.AddModelError(string.Empty, "Email không  được để trống");
                if (string.IsNullOrEmpty(cust.Phone))
                    ModelState.AddModelError(string.Empty, "Điện thoại không được để trống");

                //Kiểm tra xem có người nào đã đăng kí với tên đăng nhập này hay chưa
                var khachhang = database.Customer.FirstOrDefault(k =>
                k.username == cust.username);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng kí tên này");
                if (ModelState.IsValid)
                {
                    int maxId = database.Customer.Max(c => c.IdNguoiDung);
                    cust.IdNguoiDung = maxId + 1;
                    database.Customer.Add(cust);
                    database.SaveChanges();

                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer cust)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cust.username))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống ");
                if (string.IsNullOrEmpty(cust.password))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");

                Customer khachhang = database.Customer.Where(s => s.username == cust.username && s.password == cust.password).FirstOrDefault();
                if (khachhang != null) // Kiểm tra khachhang khác null trước khi gán vào Session
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    // Lưu vào session
                    Session["Taikhoan"] = khachhang;

                    if (khachhang.Role == "ad")
                    {
                        return RedirectToAction("ADView", "Products");
                    }
                    else
                    {
                        //return RedirectToAction("Chuyen", "CustomerProducts");
                        return RedirectToAction("Index", "CustomerProducts");
                    }
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng ";
                }
            }

            return View(cust);
        }
    }
    }