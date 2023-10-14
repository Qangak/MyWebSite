using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webatdongsan.Models;

namespace Webatdongsan.Controllers
{
    public class YeuThichController : Controller
    {
        private WebbatdongsanEntities db = new WebbatdongsanEntities();
        // GET: YeuThich
        public List<YeuThichItem> Index()
        {
            List<YeuThichItem> mYeuThich = Session["YeuThich"] as List<YeuThichItem>;
            if(mYeuThich == null)
            {
                mYeuThich = new List<YeuThichItem>();
                Session["YeuThich"] = mYeuThich;
            }
            return mYeuThich;
        }

        public ActionResult Add(int id)
        {
            List<YeuThichItem> mYeuThich = Index();
            YeuThichItem currProduct = mYeuThich.FirstOrDefault(p => p.ProductID == id);
            if(currProduct == null)
            {
                currProduct = new YeuThichItem(id);
                mYeuThich.Add(currProduct);
                Session["YeuThich"] = mYeuThich;
            }
            return RedirectToAction("Index", "CustomerProducts", new {id=id});
        }

        public ActionResult GetInfo()
        {
            if (Session["TaiKhoan"] == null) //Chưa đăng nhập
                return RedirectToAction("Login", "User");
            //else if(Session["TaiKhoan"] != null)
            //    {
            //    return RedirectToAction("GetInfo");
            //}
            List<YeuThichItem> mYeuThich = Index();
            //Trong thi tra ve
            if (mYeuThich == null || mYeuThich.Count == 0)
            {
                return RedirectToAction("Index", "CustomerProducts");
            }
            return View(mYeuThich); 
        }

        public ActionResult Remove(int id)
        {
            List<YeuThichItem> mYeuThich = Index();
            //Lấy bài viết trong yêu thích
            var currProduct = mYeuThich.FirstOrDefault(p => p.ProductID==id);
            if(currProduct != null)
            {
                mYeuThich.RemoveAll(p => p.ProductID==id);
                return RedirectToAction("Index", "CustomerProducts"); //Quay về trang chu
            }
            if(mYeuThich.Count == 0)
            {
                return RedirectToAction("Index", "CustomerProducts");
            }
            return RedirectToAction("Index", "CustomerProducts");
        }

    }
}