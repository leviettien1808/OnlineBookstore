using OnlineBookstore.Common;
using OnlineBookstore.DAL;
using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookstore.Areas.Admin.Controllers
{
    public class BookController : BaseController
    {
        private BooksAccess access = new BooksAccess();
        // GET: Admin/Book
        public ActionResult Index()
        {
            var books = access.ReadBooks();
            return View(books);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categoryAccess = new CategoriesAccess();
            ViewBag.CategoryId = new SelectList(categoryAccess.ReadCategories().Where(x => x.Id != 1), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Books books, HttpPostedFileBase image)
        {
            var categoryAccess = new CategoriesAccess();
            ViewBag.CategoryId = new SelectList(categoryAccess.ReadCategories().Where(x => x.Id != 1), "Id", "Name");
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    // Lưu hình vào thư mục
                    var filename = Server.MapPath("~/UploadFiles/" + Path.GetFileName(image.FileName));
                    image.SaveAs(filename);
                    books.Image = Path.GetFileName(image.FileName);
                }
                var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
                books.CreatedBy = sess.Username;
                books.CreatedDate = DateTime.Now;
                books.ModifiedBy = sess.Username;
                books.ModifiedDate = DateTime.Now;
                long id = access.CreateBook(books);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Book");
                }
                else if (id == 0)
                {
                    ModelState.AddModelError("", "Sách đã tồn tại");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sách thất bại");
                }
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var categoryAccess = new CategoriesAccess();
            ViewBag.BookId = new SelectList(categoryAccess.ReadCategories().Where(x => x.Id != 1), "Id", "Name", id);
            var book = access.GetBookById(id);
            return View(book);
        }

        [HttpPost]
        public ActionResult Update(Books books, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    // Lưu hình vào thư mục
                    var filename = Server.MapPath("~/UploadFiles/" + Path.GetFileName(image.FileName));
                    image.SaveAs(filename);
                    books.Image = Path.GetFileName(image.FileName);
                }
                var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
                books.ModifiedBy = sess.Username;
                bool result = access.UpdateBook(books);
                if (result)
                {
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật sách thất bại");
                }
            }
            return View("Update");
        }

        public ActionResult Delete(int id)
        {
            var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
            access.DeleteBook(id, sess.Username);
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var books = access.GetBooksInTrash();
            return View(books);
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