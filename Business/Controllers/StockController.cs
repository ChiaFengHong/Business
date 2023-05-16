using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Models;

namespace Business.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using(DBModel db = new DBModel())
            {
                List<StockList> stockList = db.StockLists.ToList<StockList>();
                return Json(new { data = stockList },JsonRequestBehavior.AllowGet);
            }
        }
    }
}