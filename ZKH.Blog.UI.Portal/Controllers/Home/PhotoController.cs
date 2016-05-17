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
    public class PhotoController : Controller
    {
        IWebInfoService webInfoService { get; set; }
        IAdvertisingInfoService advertisingInfoService { get; set; }
        IPhotoTypeService photoTypeService { get; set; }
        IPhotoInfoService photoInfoService { get; set; }
        short delFlagNormal = (short)DelFlagEnum.Normal;

        public ActionResult Index()
        {
            var typeList = photoTypeService.GetEntities(a => a.DelFlag == delFlagNormal).OrderByDescending(a => a.SubTime).ToList();
            ViewBag.Types = typeList;
            ViewBag.Advertising = advertisingInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Info = webInfoService.GetEntities(a => true).FirstOrDefault();
            return View();
        }

        public ActionResult Category(int id, int? p)
        {
            int pageIndex = p ?? 1;
            int total = 0;
            List<PhotoInfo> photos = photoInfoService.GetPageEntities(pageIndex, 30, out total, a => a.DelFlag == delFlagNormal && a.PhotoTypeId == id, a => a.SubTime, false).ToList();
            ViewBag.Types = photoTypeService.GetEntities(a => a.DelFlag == delFlagNormal).OrderByDescending(a => a.SubTime).ToList();
            ViewBag.Advertising = advertisingInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Info = webInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Photos = photos;
            ViewBag.Type = photoTypeService.GetEntities(a => a.Id == id && a.DelFlag == delFlagNormal).FirstOrDefault();
            int pageCount = (int)Math.Ceiling((double)total / 30);
            ViewBag.PageBar = Common.PageBar.GetPageBar(pageIndex, pageCount);
            return View();
        }

        public ActionResult Detail(int id)
        {
            var photo = photoInfoService.GetEntities(a => a.Id == id && a.DelFlag == delFlagNormal).FirstOrDefault();
            ViewBag.Types = photoTypeService.GetEntities(a => a.DelFlag == delFlagNormal).OrderByDescending(a => a.SubTime).ToList();
            ViewBag.Advertising = advertisingInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Info = webInfoService.GetEntities(a => true).FirstOrDefault();
            ViewBag.Photo = photo;
            ViewBag.Type = photoTypeService.GetEntities(a => a.Id == photo.PhotoTypeId).Select(a=>a.Type).FirstOrDefault();
            return View();
        }
    }
}