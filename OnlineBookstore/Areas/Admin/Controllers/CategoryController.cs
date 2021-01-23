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
    public class CategoryController : BaseController
    {
        private CategoriesAccess access = new CategoriesAccess();

        // GET: Admin/Category
        public ActionResult Index()
        {
            var categories = access.ReadCategories().Where(x => x.Id != 1);
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(access.ReadCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
                categories.CreatedBy = sess.Username;
                categories.CreatedDate = DateTime.Now;
                categories.ModifiedBy = sess.Username;
                categories.ModifiedDate = DateTime.Now;
                categories.SeoUrl = ConvertString.UTF8Convert(categories.Name);
                long id = access.CreateCategory(categories);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Category");
                }
                else if (id == 0)
                {
                    ModelState.AddModelError("", "Danh mục đã tồn tại");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm danh mục thất bại");
                }
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            // Truyền dữ liệu cho dropdown.
            ViewBag.CategoryId = new SelectList(access.ReadCategories(), "Id", "Name", id);
            var category = access.GetCategoryById(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Update(Categories categories)
        {
            if (ModelState.IsValid)
            {
                var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
                categories.ModifiedBy = sess.Username;
                categories.SeoUrl = ConvertString.UTF8Convert(categories.Name);
                bool result = access.UpdateCategory(categories);
                if (result)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật danh mục thất bại");
                }
            }
            return View("Update");
        }

        public ActionResult Delete(int id)
        {
            var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
            access.DeleteCategory(id, sess.Username);
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var categories = access.GetCategoriesInTrash();
            return View(categories);
        }

        public ActionResult ReTrash(int id)
        {
            var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
            access.ReTrash(id, sess.Username);
            return RedirectToAction("Trash");
        }

        public ActionResult Status(int id)
        {
            var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
            access.Status(id, sess.Username);
            return RedirectToAction("Index");
        }
    }
}