using GiftShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiftShopApp.Controllers
{
    public class TestController : Controller
    {
        TestDBEntities db_file = new TestDBEntities();

       // Sayfa yüklendiginde çalışacak kısımlar için başına [HttpGet] yazabiliriz.
       [HttpGet]
       public ActionResult TestAdd()
       {
            return View();
       }

       [HttpPost]
       public ActionResult TestAdd(TableProduct sended_product)
       {
            //Ado.net kullandıgımız için her tablomuza karşılık olarak burada bir adet class oluşturdu.
            //TableProduct product = new TableProduct();
            //product.ProductName = pname;
            //product.ProductPrice = Convert.ToInt32(pprice);

            // Database'deki tabloya ürünü ekledik

            if (ModelState.IsValid)
            {
                db_file.TableProduct.Add(sended_product);

                // Degişiklikleri kaydettik
                db_file.SaveChanges();
            }

           return View();
       }

        [HttpGet]
        public ActionResult TestList()
        {
            List<TableProduct> products = db_file.TableProduct.ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult TestDelete(int pid) 
        {
            TableProduct selected_product = db_file.TableProduct.Find(pid);
            db_file.TableProduct.Remove(selected_product);
            db_file.SaveChanges();
            return RedirectToAction("TestList");
        }

        [HttpGet]
        public ActionResult TestUpdate(int pid)
        {
            // Başta güncelleme işlemi için ürünün sayfamıza yüklenmesi gerekiyor. Bu yüzden
            // return View() ile ürünü sayfaya döndürdük.
            TableProduct selected_product = db_file.TableProduct.Find(pid);
            return View(selected_product);
        }

        [HttpPost]
        public ActionResult TestUpdate(TableProduct sended_product)
        {
            var selected_product = db_file.TableProduct.Find(sended_product.ProductID);
            selected_product.ProductName = sended_product.ProductName;
            selected_product.ProductPrice = sended_product.ProductPrice;
            db_file.SaveChanges();

            return RedirectToAction("TestList");
        }
    }
}