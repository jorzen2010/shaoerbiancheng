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
    public class HomeController : HomeBaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {           
                //return View();
            return RedirectToAction("CourseCenter");
           
        }

        public ActionResult CourseCenter()
        {
            ViewBag.Title = "课程中心";

            return View();
        }

        public ActionResult SetPassWord()
        {
            ViewBag.Title = "设置密码";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPassWord(FormCollection fc)
        {
            CodeUser coder = unitOfWork.codeUsersRepository.GetByID(int.Parse(Session["coderid"].ToString()));

            string oldPassword = CommonTools.ToMd5(fc["oldPassword"].ToString());
            string newPassword = fc["newPassword"];
            string repeatPassword = fc["repeatPassword"];
            string password = coder.Password;

            if (oldPassword==password && newPassword==repeatPassword)
            {
                coder.Password = CommonTools.ToMd5(fc["newPassword"].ToString());
                unitOfWork.codeUsersRepository.Update(coder);
                unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.msg = "原密码不对或两次输入的新密码不一致";
            return View();
        }

        public ActionResult CourseList(int? page, int cid)
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

            return View();
        }
       
    }
}