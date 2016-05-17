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
    public class AdminPhotoController : BaseController
    {
        IPhotoInfoService photoInfoService { get; set; }
        IPhotoTypeService photoTypeService { get; set; }
        short delFlagNormal = (short)DelFlagEnum.Normal;

        public ActionResult Index()
        {
            var typeList = photoTypeService.GetEntities(a => a.DelFlag == delFlagNormal).ToList();
            ViewBag.Types = typeList;
            return View();
        }

        public ActionResult DeleteType(string ids)
        {
            string[] strIds = ids.TrimEnd(',').Split(',');
            List<int> dellist = new List<int>();
            for (int i = 0; i < strIds.Length; i++)
            {
                dellist.Add(Int32.Parse(strIds[i]));
            }

            if (photoTypeService.DeleteListByLogical(dellist))
            {
                List<int> cids = photoInfoService.GetEntities(a => dellist.Contains(a.PhotoTypeId) && a.DelFlag == delFlagNormal).Select(a => a.Id).ToList();
                photoInfoService.DeleteListByLogical(cids);
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        [ValidateAntiForgeryToken]
        public ActionResult CreateType(PhotoType photoType)
        {
            if(photoType.Type == null)
            {
                return Content("no:相册名称不能为空 ！");
            }
            var types = photoTypeService.GetEntities(a => a.Type == photoType.Type && a.DelFlag == delFlagNormal).ToList();
            if (types.Count > 0)
            {
                return Content("no:相册名称已存在 ！");
            }

            photoType.SubTime = DateTime.Now;
            photoType.ModifiedOn = DateTime.Now;
            photoType.DelFlag = delFlagNormal;
            if(photoTypeService.Add(photoType) != null)
            {
                return Content("ok:相册创建成功 ！");
            }
            return Content("no:相册创建失败，请重试 ！");
        }

        [HttpGet]
        public ActionResult UpdateType(int id)
        {
            var type = photoTypeService.GetEntities(a => a.Id == id).FirstOrDefault();
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateType(PhotoType photoType)
        {
            if(photoType.Type == null)
            {
                return Content("no:相册名称不能为空 ！");
            }
            var types = photoTypeService.GetEntities(a => a.Type == photoType.Type && a.DelFlag == delFlagNormal && a.Id != photoType.Id).ToList();
            if (types.Count > 0)
            {
                return Content("no:相册名称已存在 ！");
            }
            var type = photoTypeService.GetEntities(a => a.Id == photoType.Id).FirstOrDefault();
            type.SubTime = photoType.SubTime;
            type.DelFlag = photoType.DelFlag;
            type.Type = photoType.Type;
            type.Remark = photoType.Remark;
            type.ModifiedOn = DateTime.Now;
            if (photoTypeService.Update(type))
            {
                return Content("ok:相册修改成功 ！");
            }
            return Content("no:相册修改失败，请重试 ！");
        }

        public ActionResult Category(int id)
        {
            var type = photoTypeService.GetEntities(a => a.Id == id).FirstOrDefault();
            var typeList = photoTypeService.GetEntities(a => a.Id != id && a.DelFlag == delFlagNormal).ToList();
            var photoList = photoInfoService.GetEntities(a => a.PhotoTypeId == id && a.DelFlag == delFlagNormal).ToList();
            ViewBag.Type = type;
            ViewBag.Types = typeList;
            ViewBag.Photos = photoList;
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult UploadPhoto()
        {
            HttpPostedFileBase file = Request.Files["photoFile"];
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

                string dir = "/UploadFiles/UploadImgs/Albums";
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
        public ActionResult Create(PhotoInfo photoInfo)
        {
            if(photoInfo.Title == null || photoInfo.Url == null)
            {
                return Content("no:图片和图片名称不能为空 ！");
            }
            var photos = photoInfoService.GetEntities(a => a.Title == photoInfo.Title && a.DelFlag == delFlagNormal && a.PhotoTypeId == photoInfo.PhotoTypeId).ToList();
            if (photos.Count > 0)
            {
                return Content("no:该图片名称已存在 ！");
            }
            photoInfo.DelFlag = delFlagNormal;
            photoInfo.SubTime = DateTime.Now;
            if (photoInfoService.Add(photoInfo) != null)
            {
                return Content("ok:图片添加成功 ！");
            }
            return Content("no:图片添加失败，请重试 ！");
        }

        public ActionResult Delete(string ids)
        {
            string[] strIds = ids.TrimEnd(',').Split(',');
            List<int> dellist = new List<int>();
            for (int i = 0; i < strIds.Length; i++)
            {
                dellist.Add(Int32.Parse(strIds[i]));
            }

            if (photoInfoService.DeleteListByLogical(dellist))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var photo = photoInfoService.GetEntities(a => a.Id == id).FirstOrDefault();
            return View(photo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PhotoInfo photoInfo)
        {
            if (photoInfo.Title == null || photoInfo.Url == null)
            {
                return Content("no:图片和图片名称不能为空 ！");
            }
            var photos = photoInfoService.GetEntities(a => a.Title == photoInfo.Title && a.DelFlag == delFlagNormal && a.Id != photoInfo.Id && a.PhotoTypeId == photoInfo.PhotoTypeId).ToList();
            if (photos.Count > 0)
            {
                return Content("no:该图片名称已存在 ！");
            }
            photoInfo.SubTime = DateTime.Now;
            if (photoInfoService.Update(photoInfo))
            {
                return Content("ok:图片修改成功 ！");
            }
            return Content("no:图片修改失败，请重试 ！");
        }

        [HttpPost]
        public ActionResult Move(string Ids, string TypeId)
        {
            if (string.IsNullOrEmpty(Ids))
            {
                return Content("no:请选择移动项 ！");
            }
            string[] strIds = Ids.TrimEnd(',').Split(',');
            List<int> movelist = new List<int>();
            for (int i = 0; i < strIds.Length; i++)
            {
                movelist.Add(Int32.Parse(strIds[i]));
            }
            int typeId = Int32.Parse(TypeId);
            if (photoInfoService.Move(movelist, typeId))
            {
                return Content("ok:图片移动成功 ！");
            }
            return Content("no:图片移动失败，请重试 ！");
        }

        public ActionResult Detail(int id)
        {
            var photo = photoInfoService.GetEntities(a => a.Id == id && a.DelFlag == delFlagNormal).FirstOrDefault();
            return View(photo);
        }
    }
}