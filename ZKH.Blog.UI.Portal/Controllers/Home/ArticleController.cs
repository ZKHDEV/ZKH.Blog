using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZKH.Blog.IBLL;
using ZKH.Blog.Model;
using ZKH.Blog.Model.Enum;

namespace ZKH.Blog.UI.Portal.Controllers.Home
{
    public class ArticleController : Controller
    {
        IArticleInfoService articleInfoService { get; set; }
        IArticleTypeService articleTypeService { get; set; }
        IAdvertisingInfoService advertisingInfoService { get; set; }
        IWebInfoService webInfoService { get; set; }
        ICommentInfoService commentInfoService { get; set; }
        short delFlagNormal = (short)DelFlagEnum.Normal;

        public ActionResult Index(int? id, int? p)
        {
            int pageIndex = p ?? 1;
            int total = 0;
            List<ArticleInfo> articles = new List<ArticleInfo>();
            if (!id.HasValue)
            {
                articles = articleInfoService.GetPageEntities(pageIndex, 10, out total, a => a.DelFlag == delFlagNormal, a => a.ModifiedOn, false).ToList();
            }
            else
            {
                articles = articleInfoService.GetPageEntities(pageIndex, 10, out total, a => a.DelFlag == delFlagNormal && a.ArticleTypeId == id, a => a.ModifiedOn, false).ToList();
                ViewBag.Type = articleTypeService.GetEntities(a => a.Id == id && a.DelFlag == delFlagNormal).FirstOrDefault();
            }
            ViewBag.Types = articleTypeService.GetEntities(a => a.DelFlag == delFlagNormal).ToList();
            ViewBag.Advertising = advertisingInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Info = webInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Articles = articles;
            //页面工具条
            int pageCount = (int)Math.Ceiling((double)total / 10);
            ViewBag.PageBar = Common.PageBar.GetPageBar(pageIndex, pageCount);
            return View();
        }

        public ActionResult Content(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
            ArticleInfo article = articleInfoService.GetEntities(a => a.Id == id && a.DelFlag == delFlagNormal).FirstOrDefault();
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type = articleTypeService.GetEntities(a => a.Id == article.ArticleTypeId && a.DelFlag == delFlagNormal).FirstOrDefault();
            ViewBag.Types = articleTypeService.GetEntities(a => a.DelFlag == delFlagNormal).ToList();
            ViewBag.Advertising = advertisingInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Info = webInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Articles = article;
            ViewBag.Comm = article.CommentInfo.Where(a => a.DelFlag == delFlagNormal).OrderByDescending(a=>a.SubTime).ToList();
            return View();
        }

        public ActionResult Comment(CommentInfo commentInfo)
        {
            if (commentInfo.Content == null || commentInfo.Name == null)
            {
                return Json(new { result = "no" }, JsonRequestBehavior.AllowGet);
            }
            commentInfo.SubTime = DateTime.Now;
            commentInfo.DelFlag = delFlagNormal;
            commentInfoService.Add(commentInfo);
            return Json(new { result="ok", com = commentInfo }, JsonRequestBehavior.AllowGet);
        }
    }
}