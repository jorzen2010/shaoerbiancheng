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
    public class CourseListController : HomeBaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

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
        //
        // GET: /CourseList/
        public ActionResult Content(int id)
        {

            VideoCourse videoCourse = unitOfWork.videoCoursesRepository.GetByID(id);

            if (videoCourse == null)
            {
                return HttpNotFound();
            }
            return View(videoCourse);
        }

	}
}