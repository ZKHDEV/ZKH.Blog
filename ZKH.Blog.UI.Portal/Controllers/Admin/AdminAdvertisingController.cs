using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZKH.Blog.IBLL;
using ZKH.Blog.Model;
using ZKH.Blog.Model.Enum;

namespace ZKH.Blog.UI.Portal.Controllers.Admin
{
    public class AdminAdvertisingController : BaseController
    {
        IAdvertisingInfoService advertisingInfoService { get; set; }
        short delFlagNormal = (short)DelFlagEnum.Normal;

        public ActionResult Index()
        {
            if (this.LoginUser.IsSuperUser == false)
            {
                return RedirectToAction("/Admin/Menu");
            }

            var advertisingInfo = advertisingInfoService.GetEntities(a => true).FirstOrDefault();
            if (advertisingInfo == null)
            {
                advertisingInfo = new AdvertisingInfo();
                advertisingInfo.SubTime = DateTime.Now;
            }
            ViewData.Model = advertisingInfo;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(AdvertisingInfo advertisingInfo)
        {
            if (this.LoginUser.IsSuperUser == false)
            {
                return RedirectToAction("/Admin/Menu");
            }

            var advertisingMsg = advertisingInfoService.GetEntities(a => true).FirstOrDefault();
            if (advertisingMsg == null)
            {
                advertisingInfo.SubTime = DateTime.Now;
                advertisingInfo.ModifiedOn = DateTime.Now;
                advertisingInfo.DelFlag = delFlagNormal;
                advertisingInfo.DueDate = DateTime.Now.AddMonths(3);
                if (advertisingInfoService.Add(advertisingInfo) != null)
                {
                    return Content("修改成功 ！");
                }
                return Content("修改失败 ！");
            }

            advertisingMsg.Url = advertisingInfo.Url;
            advertisingMsg.Link = advertisingInfo.Link;
            advertisingMsg.Signer = advertisingInfo.Signer;
            advertisingMsg.Company = advertisingInfo.Company;
            advertisingMsg.SubTime = advertisingInfo.SubTime;
            advertisingMsg.ModifiedOn = DateTime.Now;
            advertisingMsg.DelFlag = advertisingInfo.DelFlag;
            advertisingMsg.DueDate = DateTime.Now.AddMonths(3);
            
            if (advertisingInfoService.Update(advertisingMsg))
            {
                return Content("修改成功 ！");
            }
            return Content("修改失败 ！");
        }

        [ValidateAntiForgeryToken]
        public ActionResult UploadPhoto()
        {
            HttpPostedFileBase file = Request.Files["photoFile"];
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string fileExt = Path.GetExtension(fileName);
                string dir = "/UploadFiles/UploadImgs/Advertisings";
                Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));
                string newfileName = Guid.NewGuid().ToString();
                string fullDir = dir + newfileName + fileExt;
                file.SaveAs(Request.MapPath(fullDir));
                return Content("ok:" + fullDir);
            }
            else
            {
                return Content("no:请选择图片 !");
            }
        }
    }
}