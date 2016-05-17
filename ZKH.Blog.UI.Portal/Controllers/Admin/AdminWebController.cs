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
    public class AdminWebController : BaseController
    {
        IWebInfoService webInfoService { get; set; }

        public ActionResult Index()
        {
            if (this.LoginUser.IsSuperUser == false)
            {
                Response.Redirect("/Admin/Menu");
                return null;
            }

            var webInfo = webInfoService.GetEntities(a => true).FirstOrDefault();
            if (webInfo == null)
            {
                webInfo = new WebInfo();
                webInfo.SubTime = DateTime.Now;
            }
            ViewData.Model = webInfo;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(WebInfo webInfo)
        {
            if (this.LoginUser.IsSuperUser == false)
            {
                Response.Redirect("/Admin/Menu");
                return null;
            }

            var webMsg = webInfoService.GetEntities(a => true).FirstOrDefault();
            if (webMsg == null)
            {
                webInfo.SubTime = DateTime.Now;
                if (webInfoService.Add(webInfo)!=null)
                {
                    return Content("修改成功 ！");
                }
                return Content("修改失败 ！");
            }

            webMsg.Name = webInfo.Name;
            webMsg.Phone = webInfo.Phone;
            webMsg.SubTime = webInfo.SubTime;
            webMsg.Link = webInfo.Link;
            webMsg.Creator = webInfo.Creator;
            webMsg.Address = webInfo.Address;
            webMsg.Email = webInfo.Email;
            webMsg.Photo = webInfo.Photo;
            webMsg.Logo = webInfo.Logo;
            webMsg.Vedio = webInfo.Vedio;

            if (webInfoService.Update(webMsg))
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
                int size = (int)Math.Ceiling((double)file.ContentLength / 1024);
                if(size > 20000)
                {
                    return Content("no:图片不能大于20MB !");
                }
                string fileName = Path.GetFileName(file.FileName);
                string fileExt = Path.GetExtension(fileName).ToLower();

                string[] exts = new string[] { ".jpeg",".jpg",".png",".gif",".ico"};
                if (!exts.Contains(fileExt))
                {
                    return Content("no:图片格式不正确 !");
                }

                string dir = "/UploadFiles/UploadImgs/Heads";
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

        [ValidateAntiForgeryToken]
        public ActionResult UploadLogo()
        {
            HttpPostedFileBase file = Request.Files["logoFile"];
            if (file != null)
            {
                int size = (int)Math.Ceiling((double)file.ContentLength / 1024);
                if (size > 20000)
                {
                    return Content("no:图片不能大于20MB !");
                }
                string fileName = Path.GetFileName(file.FileName);
                string fileExt = Path.GetExtension(fileName).ToLower();

                string[] exts = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".ico" };
                if (!exts.Contains(fileExt))
                {
                    return Content("no:图片格式不正确 !");
                }
                string dir = "/UploadFiles/UploadImgs/Logo";
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

        public ActionResult UploadVedio()
        {
            HttpPostedFileBase file = Request.Files["vedioFile"];
            if (file != null)
            {
                int size = (int)Math.Ceiling((double)file.ContentLength / 1024);
                if (size > 50000)
                {
                    return Content("no:视频不能大于50MB !");
                }
                string fileName = Path.GetFileName(file.FileName);
                string fileExt = Path.GetExtension(fileName).ToLower();

                string[] exts = new string[] { ".mp4", ".wmv", ".flv", ".rm", "swf", "rmvb" };
                if (!exts.Contains(fileExt))
                {
                    return Content("no:视频格式不正确 !");
                }
                string dir = "/UploadFiles/UploadVedios/MainVedios";
                Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));
                string newfileName = Guid.NewGuid().ToString();
                string fullDir = dir + newfileName + fileExt;
                file.SaveAs(Request.MapPath(fullDir));
                return Content("ok:" + fullDir);
            }
            else
            {
                return Content("no:请选择视频 !");
            }
        }
    }
}