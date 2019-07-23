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
    public class VideoCourseController : AdminBaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /JkSucai/
        public ActionResult Index(int? page)
        {

            Pager pager = new Pager();
            pager.table = "VideoCourse";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<VideoCourse> dataList = DataConvertHelper<VideoCourse>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<VideoCourse>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
        }

        public ActionResult CourseList(int? page,int cid)
        {

            Pager pager = new Pager();
            pager.table = "VideoCourse";
            pager.strwhere = "Category=" + cid;
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";
            pager = CommonDal.GetPager(pager);
            IList<VideoCourse> dataList = DataConvertHelper<VideoCourse>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<VideoCourse>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);

        }

        public ActionResult Create()
        {
            CategoryService cate = new CategoryService();
            ViewData["Categorylist"] = cate.GetCategorySelectList(5);

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(VideoCourse notice)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.videoCoursesRepository.Insert(notice);
                unitOfWork.Save();
                return RedirectToAction("Index", "VideoCourse");
            }

            return View(notice);
        }

        public ActionResult Edit(int id)
        {

            VideoCourse notice = unitOfWork.videoCoursesRepository.GetByID(id);

            if (notice == null)
            {
                return HttpNotFound();
            }
            return View(notice);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(VideoCourse notice)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.videoCoursesRepository.Update(notice);
                unitOfWork.Save();
                return RedirectToAction("Index", "VideoCourse");
            }
            return View(notice);
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

                unitOfWork.videoCoursesRepository.Delete(id);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

	}
}