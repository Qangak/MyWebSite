using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webatdongsan.Models
{
    public class YeuThichItem
    {
        WebbatdongsanEntities db = new WebbatdongsanEntities();
        public int ProductID { get; set; }
        public string NamePro { get; set; }
        public string ImagePro { get; set; }
        public int Price { get; set; }
        public string LienHe{ get; set; }
        public string Address { get; set; }
        public int Number { get; set; }

        public YeuThichItem(int ProductID)
        {
            this.ProductID =ProductID;
            var productDB = db.Products.Single(s => s.ProductID == this.ProductID);
            this.NamePro = productDB.NamePro;
            this.ImagePro = productDB.ImagePro; 
            this.LienHe = productDB.LienHe;
            this.Address = productDB.Address;
            this.Number = 1;
            this.Price = (int)productDB.Price;
        }
    }
}