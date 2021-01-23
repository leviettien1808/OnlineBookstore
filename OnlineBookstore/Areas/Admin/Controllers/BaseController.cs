using OnlineBookstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineBookstore.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sess = (UserLogin) Session[CommonConstant.USER_SESSION];
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Accounts",
                        action = "Index",
                        Area = "Admin"
                    }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}