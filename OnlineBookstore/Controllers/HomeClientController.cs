using OnlineBookstore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookstore.Controllers
{
    public class HomeClientController : Controller
    {
        // GET: HomeClient
        public ActionResult Index()
        {
            ViewBag.BestSellingBook = new BooksAccess().ReadBooks().OrderByDescending(x => x.Sold).Take(10);
            ViewBag.FeaturedBook = new BooksAccess().ReadBooks().OrderByDescending(x => x.ViewCount).Take(10);
            ViewBag.DealsBook = new BooksAccess().ReadBooks().OrderByDescending(x => x.PromotionPrice).Take(10);
            return View();
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuAccess().ReadMenusByMenuTypeId(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterAccess().ReadFooters();
            return PartialView(model);
        }
    }
}