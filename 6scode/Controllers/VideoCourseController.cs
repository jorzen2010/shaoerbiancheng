using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using PagedList;
using PagedList.Mvc;
using SkyDal;
using SkyEntity;
using SkyService;
using SkyCommon;
using AliyunVideo;

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
            pager.FiledOrder = "Paixu asc";
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
        public ActionResult Create(VideoCourse videoCourse)
        {
            CategoryService cate = new CategoryService();
            ViewData["Categorylist"] = cate.GetCategorySelectList(5);
            if (ModelState.IsValid)
            {
                unitOfWork.videoCoursesRepository.Insert(videoCourse);
                unitOfWork.Save();
                return RedirectToAction("Index", "VideoCourse");
            }

            return View(videoCourse);
        }

        public ActionResult Edit(int id)
        {
            CategoryService cate = new CategoryService();
            ViewData["Categorylist"] = cate.GetCategorySelectList(5);
            VideoCourse videoCourse = unitOfWork.videoCoursesRepository.GetByID(id);

            if (videoCourse == null)
            {
                return HttpNotFound();
            }
            return View(videoCourse);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(VideoCourse videoCourse)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.videoCoursesRepository.Update(videoCourse);
                unitOfWork.Save();
                return RedirectToAction("Index", "VideoCourse");
            }
            return View(videoCourse);
        }

        public ActionResult Content(int id)
        {
            VideoCourse video = unitOfWork.videoCoursesRepository.GetByID(id);
            string ApiUrl = AliyunCommonParaConfig.ApiUrl;
            // 注意这里需要使用UTC时间，比北京时间少8小时。
            string Timestamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", DateTimeFormatInfo.InvariantInfo);
            string Action = "GetVideoPlayAuth";
            string SignatureNonce = CommonTools.EncryptToSHA1(CommonTools.GenerateRandomNumber(8));

            //  string VideoId = "6ccf973fe06741e49ab849d4cec017e0";

            string VideoId = video.VideoId;
            ViewBag.VideoId = VideoId;

            ViewBag.PlayAuth = AliyunVideoServices.GetVideoInfo(ApiUrl, VideoId, Timestamp, Action, SignatureNonce).PlayAuth;

            return View(video);
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