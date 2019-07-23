using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _6scode.Controllers
{
    public class HomeController : AdminBaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CourseList()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Content()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}