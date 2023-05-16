using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Models;

namespace Business.Controllers
{
    public class AddItemController : Controller
    {
        // GET: AddItem
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddOrEditKind(int id = 0)
        {
            KindList kindList = new KindList();
            if (id == 0)
            {
                return View(new KindList());
            }
            else
            {
                using (DBModel db = new DBModel())
                {
                    kindList = db.KindLists.Where(x => x.KindID == id).FirstOrDefault<KindList>();
                    return View(kindList);
                }
            }
            
        }
        
        [HttpPost]
        public ActionResult AddOrEditKind(KindList kindList)
        {
            using (DBModel db = new DBModel())
            {
                if (kindList.KindID == 0) {
                    if (kindList.CreateTime == null)
                    {
                        kindList.CreateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        kindList.ModifyTime = kindList.CreateTime;
                    }                    

                    db.KindLists.Add(kindList);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(kindList).State = EntityState.Modified;
                    kindList.ModifyTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    List<ItemList> itemlists = db.ItemLists.Where(x => x.KindID == kindList.KindID).ToList<ItemList>();
                    List<AddItem> productLists = db.AddItems.Where(x => x.KindID == kindList.KindID).ToList<AddItem>();
                    foreach (var itemlist in itemlists)
                    {
                        itemlist.KindName = kindList.KindName;                        
                    }
                    foreach (var productList in productLists)
                    {
                        productList.KindName = kindList.KindName;
                    }
                    db.SaveChanges();

                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }            
        }

        [HttpGet]
        public ActionResult AddOrEditItem(int id = 0)
        {
            using (DBModel db = new DBModel())
            {
                ItemList itemList = new ItemList();
                itemList.KindListCollection = db.KindLists.ToList<KindList>();
                
                if (id == 0)
                {                  
                    return View(itemList);
                }
                else
                {
                    itemList = db.ItemLists.Where(x => x.ItemID == id).FirstOrDefault<ItemList>();
                    itemList.KindListCollection = db.KindLists.ToList<KindList>();
                    return View(itemList);
                }
            }

        }

        [HttpPost]
        public ActionResult AddOrEditItem(ItemList itemList)
        {
            using (DBModel db = new DBModel())
            {
                if (itemList.ItemID == 0)
                {
                    if (itemList.CreateTime == null)
                    {                        
                        itemList.KindID = db.KindLists.Where(x => x.KindName == itemList.KindName).FirstOrDefault().KindID;
                        itemList.CreateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        itemList.ModifyTime = itemList.CreateTime;
                    }
                    db.ItemLists.Add(itemList);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(itemList).State = EntityState.Modified;
                    itemList.ModifyTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    List<AddItem> productLists = db.AddItems.Where(x => x.ItemID == itemList.ItemID).ToList<AddItem>();

                    foreach (var productList in productLists)
                    {
                        productList.ItemName = itemList.ItemName;
                    }
                    db.SaveChanges();

                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }
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

        [HttpGet]
        public ActionResult AddOrEditProduct(int id = 0)
        {
            using (DBModel db = new DBModel())
            {
                AddItem productList = new AddItem();
                productList.KindListCollection = db.KindLists.ToList<KindList>();
                if (id == 0)
                {  
                    return View(productList);
                }
                else
                {
                    productList = db.AddItems.Where(x => x.ProductID == id).FirstOrDefault<AddItem>();
                    productList.KindListCollection = db.KindLists.ToList<KindList>();
                    return View(productList);                    
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEditProduct(AddItem productList)
        {
            using (DBModel db = new DBModel())
            {
                if (productList.ProductID == 0)
                {
                    if (productList.CreateTime == null)
                    {
                        productList.KindID = db.KindLists.Where(x => x.KindName == productList.KindName).FirstOrDefault().KindID;
                        productList.ItemID = db.ItemLists.Where(x => x.ItemName == productList.ItemName).FirstOrDefault().ItemID;
                        productList.CreateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        productList.ModifyTime = productList.CreateTime;
                    }
                    db.AddItems.Add(productList);

                    StockList stockList = new StockList();                    
                    stockList.KindName = productList.KindName;
                    stockList.ItemName = productList.KindName;
                    stockList.ProductName = productList.ProductName;
                    stockList.CreateTime = productList.CreateTime;
                    stockList.ModifyTime = stockList.CreateTime;
                    db.StockLists.Add(stockList);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(productList).State = EntityState.Modified;
                    productList.ModifyTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    productList.KindID = db.KindLists.Where(x => x.KindName == productList.KindName).FirstOrDefault().KindID;
                    productList.ItemID = db.ItemLists.Where(x => x.ItemName == productList.ItemName).FirstOrDefault().ItemID;

                    StockList stockList = db.StockLists.Where(x => x.StockID == productList.ProductID).FirstOrDefault<StockList>();
                    stockList.ProductName = productList.ProductName;
                    stockList.KindName = productList.KindName;
                    stockList.ItemName = productList.ItemName;

                    db.SaveChanges();

                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            using (DBModel db = new DBModel())
            {
                AddItem productList = db.AddItems.Where(x => x.ProductID == id).FirstOrDefault<AddItem>();
                db.AddItems.Remove(productList);
                StockList stockList = db.StockLists.Where(x => x.StockID == id).FirstOrDefault<StockList>();
                db.StockLists.Remove(stockList);
                
                db.SaveChanges();

                return Json(new { success = true, message = "Delete Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetKindData()
        {
            using (DBModel db = new DBModel())
            {
                List<KindList> kindList = db.KindLists.ToList<KindList>();
                return Json(new { data = kindList }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetItemData()
        {
            using (DBModel db = new DBModel())
            {
                List<ItemList> itemList = db.ItemLists.ToList<ItemList>();
                return Json(new { data = itemList }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetProductData()
        {
            using (DBModel db = new DBModel())
            {
                List<AddItem> productList = db.AddItems.ToList<AddItem>();
                return Json(new { data = productList }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}