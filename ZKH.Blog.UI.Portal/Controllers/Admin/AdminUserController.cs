using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZKH.Blog.IBLL;
using ZKH.Blog.Model;
using ZKH.Blog.Model.Enum;

namespace ZKH.Blog.UI.Portal.Controllers.Admin
{
    public class AdminUserController : BaseController
    {
        IAdminInfoService adminInfoService { get; set; }
        IAdminInfoExtService adminInfoExtService { get; set; }
        DelFlagEnum delFlagNormal = DelFlagEnum.Normal;

        public ActionResult Index()
        {
            var adminInfolist = adminInfoService.GetEntities(a => a.DelFlag == (short)delFlagNormal).ToList();
            ViewData["adminInfo"] = adminInfolist;
            return View();
        }

        public ActionResult Detail(int id)
        {
            var adminInfoExt = adminInfoExtService.GetEntities(a => a.AdminInfoId == id).FirstOrDefault();
            return View(adminInfoExt);
        }

        public ActionResult Delete(string ids)
        {
            string[] strIds = ids.TrimEnd(',').Split(',');
            List<int> dellist = new List<int>();
            for (int i = 0; i < strIds.Length; i++)
            {
                dellist.Add(Int32.Parse(strIds[i]));
            }
            List<int> extids = adminInfoExtService.GetEntities(a => dellist.Contains(a.AdminInfoId)).Select(a => a.Id).ToList();

            if (adminInfoService.DeleteListByLogical(dellist) && adminInfoExtService.DeleteListByLogical(extids))
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
            var adminInfo = adminInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewData.Model = adminInfo;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(AdminInfo adminInfo)
        {
            adminInfo.ModifiedOn = DateTime.Now;

            if (adminInfoService.Update(adminInfo))
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