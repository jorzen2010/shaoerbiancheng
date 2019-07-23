using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkyDal;
using SkyEntity;
using SkyService;
using SkyCommon;

namespace _6scode.Controllers
{
    public class CourseListController : Controller
    {
        //
        // GET: /CourseList/
        public ActionResult Index()
        {
            return View();
        }
	}
}