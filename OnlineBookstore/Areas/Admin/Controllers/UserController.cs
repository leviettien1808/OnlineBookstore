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
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            var usersAccess = new UsersAccess();
            var users = usersAccess.ReadUsers();
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Users users)
        {
            if (ModelState.IsValid)
            {
                var user = new UsersAccess();
                users.Password = Encryptor.MD5Hash(users.Password);
                var sess = (UserLogin) Session[CommonConstant.USER_SESSION];
                users.CreatedBy = sess.Username;
                users.CreatedDate = DateTime.Now;
                users.ModifiedBy = sess.Username;
                users.ModifiedDate = DateTime.Now;
                long id = user.CreateUser(users);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else if (id == 0)
                {
                    ModelState.AddModelError("", "Tên tài khoản đã tồn tại");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm người dùng thất bại");
                }
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var usersAccess = new UsersAccess();
            var user = usersAccess.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Update(Users users)
        {
            if (ModelState.IsValid)
            {
                var user = new UsersAccess();
                if (!string.IsNullOrEmpty(users.Password))
                {
                    users.Password = Encryptor.MD5Hash(users.Password);
                }
                
                var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
                users.ModifiedBy = sess.Username;
                bool result = user.UpdateUser(users);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật người dùng thất bại");
                }
            }
            return View("Update");
        }

        public ActionResult Delete(int id)
        {
            var usersAccess = new UsersAccess();
            var user = usersAccess.GetUserById(id);
            var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
            user.ModifiedBy = sess.Username;
            var result = usersAccess.DeleteUser(id);
            if (result)
            {
                ViewBag.Message = "Xóa " + user.Username + " thành công";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var usersAccess = new UsersAccess();
            var users = usersAccess.GetUsersInTrash();
            return View(users);
        }

        public ActionResult ReTrash(int id)
        {
            var usersAccess = new UsersAccess();
            var user = usersAccess.GetUserById(id);
            var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
            user.ModifiedBy = sess.Username;
            usersAccess.ReTrash(id);
            return RedirectToAction("Trash");
        }

        public ActionResult Status(int id)
        {
            var usersAccess = new UsersAccess();
            var sess = (UserLogin)Session[CommonConstant.USER_SESSION];
            usersAccess.Status(id, sess.Username);
            return RedirectToAction("Index");
        }
    }
}