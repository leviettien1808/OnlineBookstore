using OnlineBookstore.Common;
using OnlineBookstore.DAL;
using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookstore.Areas.Admin.Controllers
{
    public class MenuTypeController : BaseController
    {
        private MenuTypeAccess access = new MenuTypeAccess();
        // GET: Admin/MenuType
        public ActionResult Index()
        {
            var menuTypes = access.ReadMenuTypes();
            return View(menuTypes);
        }

        public ActionResult Status(int id)
        {
            var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
            access.Status(id, sess.Username);
            return RedirectToAction("Index");
        }
    }
}