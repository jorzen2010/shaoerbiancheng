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
    public class CodeUserController : AdminBaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /JkSucai/
        public ActionResult Index(int? page)
        {

            Pager pager = new Pager();
            pager.table = "CodeUser";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<CodeUser> dataList = DataConvertHelper<CodeUser>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<CodeUser>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
        }

        public ActionResult CourseList(int? page,int cid)
        {

            Pager pager = new Pager();
            pager.table = "CodeUser";
            pager.strwhere = "Category=" + cid;
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Paixu asc";
            pager = CommonDal.GetPager(pager);
            IList<CodeUser> dataList = DataConvertHelper<CodeUser>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<CodeUser>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);

        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CodeUser codeUser)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.codeUsersRepository.Insert(codeUser);
                unitOfWork.Save();
                return RedirectToAction("Index", "CodeUser");
            }

            return View(codeUser);
        }

        public ActionResult Edit(int id)
        {
            CategoryService cate = new CategoryService();
            ViewData["Categorylist"] = cate.GetCategorySelectList(5);
            CodeUser codeUser = unitOfWork.codeUsersRepository.GetByID(id);

            if (codeUser == null)
            {
                return HttpNotFound();
            }
            return View(codeUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CodeUser codeUser)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.codeUsersRepository.Update(codeUser);
                unitOfWork.Save();
                return RedirectToAction("Index", "CodeUser");
            }
            return View(codeUser);
        }

        public ActionResult Content(int id)
        {

            CodeUser codeUser = unitOfWork.codeUsersRepository.GetByID(id);

            if (codeUser == null)
            {
                return HttpNotFound();
            }
            return View(codeUser);
        }

        //彻底删除
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int? id)
        {
            Message msg = new Message();
            if (id == null)
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "找不到ID";
            }
            else
            {

                unitOfWork.codeUsersRepository.Delete(id);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

	}
}