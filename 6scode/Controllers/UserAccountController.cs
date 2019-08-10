using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Data;
using System.Collections;
using SkyDal;
using SkyEntity;
using SkyCommon;

namespace _6scode.Controllers
{
     [AllowAnonymous]
    public class UserAccountController : Controller
    {
        private SkyWebContext db = new SkyWebContext();

        //
        // GET: /Account/
        public ActionResult Login()
        {

            if (!string.IsNullOrEmpty(Request["returnUrl"]))
            {

                ViewBag.returnUrl = Request["returnUrl"].ToString();
                ViewBag.msg = "对不起，您尚未登陆不允许访问！";

            }

            return View();
        }

        public ActionResult Logout()
        {

            Response.Cookies["codername"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["coderid"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Session["coderid"] = null;
            System.Web.HttpContext.Current.Session["codername"] = null;
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            System.Web.HttpContext.Current.Session["codername"] = "";
            string username = fc["UserName"];
            string password = CommonTools.ToMd5(fc["Password"].ToString());
            bool rememberme = fc["RememberMe"] == null ? false : true;
            string reurnUrl = fc["ReturnUrl"];




            var codeUsers = from s in db.CodeUsers
                           orderby s.Id ascending
                           where (s.UserName == username && s.Password == password)
                           select s;
            if (codeUsers.Count() > 0)
            {
                if (codeUsers.First().UserStatus)
                {
                    HttpCookie cookie = new HttpCookie("codername");
                    cookie.Value = username;
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

                    HttpCookie cookieid = new HttpCookie("coderid");
                    cookieid.Value = codeUsers.First().UserStatus.ToString();
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookieid);

                    System.Web.HttpContext.Current.Session["coderid"] = codeUsers.First().Id.ToString();
                    System.Web.HttpContext.Current.Session["codername"] = username;

                    FormsAuthentication.SetAuthCookie(username, rememberme);

                    if (string.IsNullOrEmpty(reurnUrl))
                    {
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Session["codername"] = null;
                        ViewBag.msg = "没有权限";
                        return Redirect(reurnUrl);
                    }

                }

                else
                {
                    System.Web.HttpContext.Current.Session["codername"] = null;
                    ViewBag.msg = "此已经被禁用，不允许登陆";
                    return View();

                }

            }
            else
            {
                System.Web.HttpContext.Current.Session["codername"] = null;
                ViewBag.msg = "用户名或密码错误了";
                return View();

            }


        }
	}
}