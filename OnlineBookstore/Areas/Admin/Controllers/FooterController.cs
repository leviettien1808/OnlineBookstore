using OnlineBookstore.Common;
using OnlineBookstore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookstore.Areas.Admin.Controllers
{
    public class FooterController : Controller
    {
        FooterAccess access = new FooterAccess();
        // GET: Admin/Footer
        public ActionResult Index()
        {
            var footer = access.ReadFooters();
            return View(footer);
        }

        public ActionResult Status(string id)
        {
            access.Status(id);
            return RedirectToAction("Index");
        }
    }
}