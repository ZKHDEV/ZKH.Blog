using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZKH.Blog.IBLL;
using ZKH.Blog.Model;
using ZKH.Blog.Model.Enum;

namespace ZKH.Blog.UI.Portal.Controllers.Admin
{
    public class AdminArticleController : BaseController
    {
        IAdminInfoExtService adminInfoExtService { get; set; }
        IArticleInfoService articleInfoService { get; set; }
        IArticleTypeService articleTypeService { get; set; }
        ICommentInfoService commentInfoService { get; set; }
        short delFlagNormal = (short)DelFlagEnum.Normal;

        public ActionResult Index()
        {
            var articleInfoList = articleInfoService.GetEntities(a => a.DelFlag == delFlagNormal).ToList();
            var articleTypeList = articleTypeService.GetEntities(a => a.DelFlag == delFlagNormal).ToList();
            ViewData["articles"] = articleInfoList;
            ViewData["types"] = articleTypeList;

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var articleTypeList = articleTypeService.GetEntities(a => a.DelFlag == delFlagNormal).ToList();
            ViewData["types"] = articleTypeList;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]   //使用富文本编辑器时需加该字段
        public ActionResult Create(ArticleInfo articleInfo)
        {
            articleInfo.SubTime = DateTime.Now;
            articleInfo.ModifiedOn = DateTime.Now;
            articleInfo.DelFlag = delFlagNormal;
            articleInfo.Author = adminInfoExtService.GetEntities(a => a.AdminInfoId == this.LoginUser.Id).FirstOrDefault().Name;

            if (articleInfoService.Add(articleInfo) != null)
            {
                return Content("ok");
            }
            return Content("no");
        }

        public ActionResult Detail(int id)
        {
            var articleInfo = articleInfoService.GetEntities(a => a.Id == id).FirstOrDefault();
            ViewData.Model = articleInfo;
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

            if (articleInfoService.DeleteListByLogical(dellist))
            {
                List<int> cids = commentInfoService.GetEntities(a => dellist.Contains(a.ArticleInfoId) && a.DelFlag == delFlagNormal).Select(a => a.Id).ToList();
                commentInfoService.DeleteListByLogical(cids);
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult DelType(int id)
        {
            List<int> ids = articleInfoService.GetEntities(a => a.ArticleTypeId == id && a.DelFlag == delFlagNormal).Select(a => a.Id).ToList();
            List<int> cids = commentInfoService.GetEntities(a => ids.Contains(a.ArticleInfoId) && a.DelFlag == delFlagNormal).Select(a => a.Id).ToList();
            List<int> typeId = new List<int>();
            typeId.Add(id);
            if (articleTypeService.DeleteListByLogical(typeId)) 
            {
                articleInfoService.DeleteListByLogical(ids);
                commentInfoService.DeleteListByLogical(cids);
                return Content("ok");
            }
            return Content("no");
        }

        public ActionResult CreType(string Type)
        {
            if (Type == "")
            {
                return Content("no");
            }
            if(articleTypeService.GetEntities(a=> a.DelFlag==delFlagNormal && a.Type.ToLower() == Type.ToLower()).ToList().Count() > 0)
            {
                return Content("no1");
            }

            ArticleType articleType = new ArticleType();
            articleType.Type = Type;
            articleType.DelFlag = delFlagNormal;

            if (articleTypeService.Add(articleType) != null) 
            {
                return Content("ok");
            }
            return Content("no");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var articleInfo = articleInfoService.GetEntities(a => a.Id == id).FirstOrDefault();
            var articleTypeList = articleTypeService.GetEntities(a => a.DelFlag == delFlagNormal).ToList();
            ViewData["types"] = articleTypeList;
            ViewData.Model = articleInfo;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ArticleInfo article)
        {
            //具有导航属性
            ArticleInfo articleInfo = articleInfoService.GetEntities(a => a.Id == article.Id).FirstOrDefault();
            articleInfo.ModifiedOn = DateTime.Now;
            articleInfo.Title = article.Title;
            articleInfo.Content = article.Content;
            articleInfo.Summary = article.Summary;
            articleInfo.ArticleTypeId = article.ArticleTypeId;
            articleInfo.Author = adminInfoExtService.GetEntities(a => a.AdminInfoId == this.LoginUser.Id).FirstOrDefault().Name;

            if (articleInfoService.Update(articleInfo)) 
            {
                return Content("ok");
            }
            return Content("no");
        }
    }
}