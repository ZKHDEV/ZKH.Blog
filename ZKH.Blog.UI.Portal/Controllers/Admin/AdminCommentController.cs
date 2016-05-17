using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZKH.Blog.IBLL;
using ZKH.Blog.Model.Enum;

namespace ZKH.Blog.UI.Portal.Controllers.Admin
{
    public class AdminCommentController : BaseController
    {
        IArticleInfoService articleInfoService { get; set; }
        ICommentInfoService commentInfoService { get; set; }
        short delFlagNormal = (short)DelFlagEnum.Normal;

        public ActionResult Index()
        {
            var articleInfoList = articleInfoService.GetEntities(a => a.DelFlag == delFlagNormal).ToList();
            var commentList = commentInfoService.GetEntities(a => a.DelFlag == delFlagNormal).OrderByDescending(a => a.SubTime).ToList();
   
            ViewData["articles"] = articleInfoList;
            ViewData["coms"] = commentList;
            return View();
        }

        public ActionResult Delete(string ids)
        {
            string[] strIds = ids.TrimEnd(',').Split(',');
            List<int> dellist = new List<int>();
            for (int i = 0; i < strIds.Length; i++)
            {
                dellist.Add(Int32.Parse(strIds[i]));
            }

            if (commentInfoService.DeleteListByLogical(dellist))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
    }
}