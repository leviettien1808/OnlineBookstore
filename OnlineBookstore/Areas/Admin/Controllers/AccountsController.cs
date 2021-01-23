using OnlineBookstore.Areas.Admin.Data;
using OnlineBookstore.Common;
using OnlineBookstore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookstore.Areas.Admin.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Admin/Accounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn(SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var user = new UsersAccess();
                // kiểm tra người dùng là admin hay customer
                var check = user.GetUserByUsername(signInModel.Username);
                if (check.Authorization == 1)
                {
                    var result = user.SignIn(signInModel.Username, Encryptor.MD5Hash(signInModel.Password));
                    if (result == 1)
                    {
                        var u = user.GetUserByUsername(signInModel.Username);
                        var userSession = new UserLogin();
                        userSession.Username = u.Username;
                        userSession.UserId = u.Id;
                        Session.Add(CommonConstant.USER_SESSION, userSession);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result == 0)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                    }
                    else if (result == 2)
                    {
                        ModelState.AddModelError("", "Tài khoản đã bị khóa");
                    }
                    else if (result == -1)
                    {
                        ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng nhập không thành công");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bạn không có quyền đăng nhập");
                }
            }
            return View("Index");
        }

        public ActionResult SignOut()
        {
            var userSession = new UserLogin();
            userSession.Username = "";
            userSession.UserId = -1;
            Session.Add(CommonConstant.USER_SESSION, userSession);
            return View("Index");
        }
    }
}