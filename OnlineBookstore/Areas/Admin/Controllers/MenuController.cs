using OnlineBookstore.DAL;
using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookstore.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        private MenuAccess access = new MenuAccess();

        // GET: Admin/Category
        public ActionResult Index()
        {
            var menus = access.ReadMenus().Where(x => x.Id != 1);
            return View(menus);
        }

        [HttpGet]
        public ActionResult Create()
        {
            MenuTypeAccess menuTypeAccess = new MenuTypeAccess();
            ViewBag.MenuTypeId = new SelectList(menuTypeAccess.ReadMenuTypes(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Menus menu)
        {
            if (ModelState.IsValid)
            {
                long id = access.CreateMenu(menu);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Menu");
                }
                else if (id == 0)
                {
                    ModelState.AddModelError("", "Menu đã tồn tại");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm menu thất bại");
                }
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            MenuTypeAccess menuTypeAccess = new MenuTypeAccess();
            // Truyền dữ liệu cho dropdown.
            ViewBag.MenuTypeId = new SelectList(menuTypeAccess.ReadMenuTypes(), "Id", "Name", id);
            var menu = access.GetMenuById(id);
            return View(menu);
        }

        [HttpPost]
        public ActionResult Update(Menus menu)
        {
            if (ModelState.IsValid)
            {
                bool result = access.UpdateMenu(menu);
                if (result)
                {
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật menu thất bại");
                }
            }
            return View("Update");
        }

        public ActionResult Delete(int id)
        {
            access.DeleteMenu(id);
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var menus = access.GetMenusInTrash();
            return View(menus);
        }

        public ActionResult ReTrash(int id)
        {
            access.ReTrash(id);
            return RedirectToAction("Trash");
        }

        public ActionResult Status(int id)
        {
            access.Status(id);
            return RedirectToAction("Index");
        }
    }
}