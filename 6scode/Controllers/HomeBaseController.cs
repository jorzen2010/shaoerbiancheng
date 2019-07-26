using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkyDal;

namespace _6scode.Controllers
{
    public class HomeBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["codername"] == null)
            {
                filterContext.Result = Redirect("/UserAccount/Login");
            }
            ViewBag.Title = "陪伴空间";
        }
    }
}