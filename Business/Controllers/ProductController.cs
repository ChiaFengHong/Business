using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Business.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddPurchasing()
        {
            using (DBModel db = new DBModel())
            {
                PurchaseLog purchaseLog = new PurchaseLog();
                purchaseLog.KindListCollection = db.KindLists.ToList<KindList>();
                
                return View(purchaseLog);
            }
        }

        [HttpPost]
        public ActionResult AddPurchasing(PurchaseLog purchaseLog)
        {
            using (DBModel db = new DBModel())
            {
                purchaseLog.CreateTime = purchaseLog.CreateTime == null ? DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") : purchaseLog.CreateTime;                
                purchaseLog.TotalPrice = purchaseLog.Qty * purchaseLog.UnitPrice;
                db.PurchaseLogs.Add(purchaseLog);

                StockList stockList = db.StockLists.Where(x => x.ProductName == purchaseLog.ProductName).FirstOrDefault<StockList>();
                stockList.Qty += purchaseLog.Qty;
                stockList.ModifyTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                db.SaveChanges();

                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);                             
            }
        }

        public ActionResult AddShipping()
        {
            using (DBModel db = new DBModel())
            {
                ShippingLog shippingLog = new ShippingLog();
                shippingLog.KindListCollection = db.KindLists.ToList<KindList>();

                return View(shippingLog);
            }
        }

        [HttpPost]
        public ActionResult AddShipping(ShippingLog shippingLog)
        {
            using (DBModel db = new DBModel())
            {
                shippingLog.CreateTime = shippingLog.CreateTime == null ? DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") : shippingLog.CreateTime;
                shippingLog.TotalPrice = shippingLog.Qty * shippingLog.UnitPrice;
                db.ShippingLogs.Add(shippingLog);

                StockList stockList = db.StockLists.Where(x => x.ProductName == shippingLog.ProductName).FirstOrDefault<StockList>();
                stockList.Qty -= shippingLog.Qty;
                stockList.ModifyTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                db.SaveChanges();

                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPurchasingData()
        {
            using (DBModel db = new DBModel())
            {
                List<PurchaseLog> purchasingList = db.PurchaseLogs.ToList<PurchaseLog>();
                return Json(new { data = purchasingList }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetShippingData()
        {
            using (DBModel db = new DBModel())
            {
                List<ShippingLog> shippingLogList = db.ShippingLogs.ToList<ShippingLog>();
                return Json(new { data = shippingLogList }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetItemListByKindName(string KindName)
        {
            using (DBModel db = new DBModel())
            {
                db.Configuration.ProxyCreationEnabled = false;
                List<ItemList> itemLists = db.ItemLists.Where(x => x.KindName == KindName).ToList<ItemList>();
                return Json(itemLists, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetProductListByItemName(string ItemName)
        {
            using (DBModel db = new DBModel())
            {
                db.Configuration.ProxyCreationEnabled = false;
                List<AddItem> ProductLists = db.AddItems.Where(x => x.ItemName == ItemName).ToList<AddItem>();
                return Json(ProductLists, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeletePurchase(int id)
        {
            using (DBModel db = new DBModel())
            {
                PurchaseLog purchaseLog = db.PurchaseLogs.Where(x => x.PurchaseID == id).FirstOrDefault<PurchaseLog>();
                db.PurchaseLogs.Remove(purchaseLog);

                StockList stockList = db.StockLists.Where(x => x.ProductName == purchaseLog.ProductName).FirstOrDefault<StockList>();
                stockList.Qty -= purchaseLog.Qty;
                stockList.ModifyTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                db.SaveChanges();

                return Json(new { success = true, message = "Delete Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteShipping(int id)
        {
            using (DBModel db = new DBModel())
            {
                ShippingLog shippingLog = db.ShippingLogs.Where(x => x.ShippingID == id).FirstOrDefault<ShippingLog>();
                db.ShippingLogs.Remove(shippingLog);

                StockList stockList = db.StockLists.Where(x => x.ProductName == shippingLog.ProductName).FirstOrDefault<StockList>();
                stockList.Qty += shippingLog.Qty;
                stockList.ModifyTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                db.SaveChanges();

                return Json(new { success = true, message = "Delete Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}