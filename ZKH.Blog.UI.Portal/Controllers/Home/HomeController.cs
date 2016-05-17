using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZKH.Blog.Common;
using ZKH.Blog.IBLL;
using ZKH.Blog.Model;
using ZKH.Blog.Model.Enum;

namespace ZKH.Blog.UI.Portal.Controllers.Home
{
    public class HomeController : Controller
    {
        IArticleInfoService articleInfoService { get; set; }
        IAdminInfoExtService adminInfoExtService { get; set; }
        IAdvertisingInfoService advertisingInfoService { get; set; }
        IWebInfoService webInfoService { get; set; }
        IMessageInfoService messageInfoService { get; set; }
        IPhotoInfoService photoInfoService { get; set; }
        short delFlagNormal = (short)DelFlagEnum.Normal;

        public ActionResult Index()
        {
            ViewBag.Advertising = advertisingInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Info = webInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Articles = articleInfoService.GetEntities(a => a.DelFlag == delFlagNormal).OrderByDescending(a=>a.ModifiedOn).ToList();
            ViewBag.Photos = photoInfoService.GetEntities(a => a.DelFlag == delFlagNormal).OrderByDescending(a => a.SubTime).ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Advertising = advertisingInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Info = webInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Articles = articleInfoService.GetEntities(a => a.DelFlag == delFlagNormal).ToList();
            ViewBag.Photos = photoInfoService.GetEntities(a => a.DelFlag == delFlagNormal).OrderByDescending(a => a.SubTime).ToList();
            return View();
        }

        public ActionResult Author()
        {
            ViewBag.Advertising = advertisingInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Info = webInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Articles = articleInfoService.GetEntities(a => a.DelFlag == delFlagNormal).ToList();
            ViewBag.Author = adminInfoExtService.GetEntities(a => a.DelFlag == delFlagNormal).FirstOrDefault();
            ViewBag.Photos = photoInfoService.GetEntities(a => a.DelFlag == delFlagNormal).OrderByDescending(a => a.SubTime).ToList();
            return View();
        }

        public ActionResult Connect()
        {
            ViewBag.Advertising = advertisingInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Info = webInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Articles = articleInfoService.GetEntities(a => a.DelFlag == delFlagNormal).ToList();
            ViewBag.Msg = messageInfoService.GetEntities(a => a.DelFlag == delFlagNormal).OrderByDescending(a => a.SubTime).ToList();
            ViewBag.Photos = photoInfoService.GetEntities(a => a.DelFlag == delFlagNormal).OrderByDescending(a => a.SubTime).ToList();
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult Message(MessageInfo messageInfo)
        {
            if (messageInfo.Content == null || messageInfo.Name == null || messageInfo.Email == null)
            {
                return Json(new { result = "no" }, JsonRequestBehavior.AllowGet);
            }
            messageInfo.SubTime = DateTime.Now;
            messageInfo.DelFlag = delFlagNormal;
            messageInfoService.Add(messageInfo);
            return Json(new { result = "ok", com = messageInfo }, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Email(string Name, string Title, string Econtent)
        {
            if(Name == null || Econtent == null || Title == null)
            {
                return Content("称呼、标题或内容不能为空 ！");
            }
            Econtent = Name + " : " + Econtent;
            string mailTo = ConfigurationManager.AppSettings["feedbackEmail"];

            if(EmailHelper.SendEmail(mailTo, Title, Econtent))
            {
                return Content("发送成功 ！");
            }

            return Content("发送失败，请重试 ！");
        }
        
    }
}