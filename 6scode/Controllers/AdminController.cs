using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using SkyDal;
using SkyEntity;
using SkyService;
using SkyCommon;

namespace _6scode.Controllers
{
    public class AdminController : AdminBaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {           
                return View();
           
        }

      
       
    }
}