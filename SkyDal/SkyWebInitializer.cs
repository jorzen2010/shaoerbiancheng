using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SkyEntity;
using SkyCommon;

namespace SkyDal
{
    public class SkyWebInitializer : DropCreateDatabaseIfModelChanges<SkyWebContext>
    {
        protected override void Seed(SkyWebContext context)
        {
            base.Seed(context);

            

            #region 默认分类

            var CategoryList = new List<Category>
            {
                new Category{
                    CategoryName="根目录",
                    CategoryInfo="根目录",
                    CategoryParentID=0,
                    CategoryStatus=true,
                    CategorySort=0,

                },
                new Category{
                    CategoryName="文章分类",
                    CategoryInfo="文章分类",
                    CategoryParentID=1,
                    CategoryStatus=true,
                    CategorySort=0,

                },
                new Category{
                    CategoryName="系统目录",
                    CategoryInfo="系统目录",
                    CategoryParentID=2,
                    CategoryStatus=true,
                    CategorySort=0,
                },
                new Category{
                    CategoryName="通知公告",
                    CategoryInfo="通知公告",
                    CategoryParentID=2,
                    CategoryStatus=true,
                    CategorySort=0,
                },
            };
            CategoryList.ForEach(j => context.Categorys.Add(j));
            context.SaveChanges();
            #endregion 
            


        }
    }
}
