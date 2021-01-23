using OnlineBookstore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookstore.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult BookCategory()
        {
            var model = new CategoriesAccess().ReadCategories().Where(x => x.Id != 1);
            return PartialView(model);
        }
    }
}