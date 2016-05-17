using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZKH.Blog.Common.Cache;
using ZKH.Blog.IBLL;
using ZKH.Blog.Model;
using ZKH.Blog.Model.Enum;

namespace ZKH.Blog.UI.Portal.Controllers.Admin
{
    public class AdminPerMsgController : BaseController
    {
        IAdminInfoService adminInfoService { get; set; }
        IAdminInfoExtService adminInfoExtService { get; set; }
        short delFlagNormal = (short)DelFlagEnum.Normal;

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.UName = adminInfoService.GetEntities(a => a.Id == this.LoginUser.Id).FirstOrDefault().UName;
            var adminInfoExt = adminInfoExtService.GetEntities(a => a.AdminInfoId == LoginUser.Id).FirstOrDefault();
            return View(adminInfoExt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AdminInfoExt adminInfoExt)
        {
            string uName = Request["UName"].ToString();
            ViewBag.UName = uName;

            if (!ModelState.IsValid)
            {
                return View(adminInfoExt);
            }

            //this.LoginUser中不包含导航属性
            AdminInfo adminInfo = adminInfoService.GetEntities(a => a.Id == this.LoginUser.Id).FirstOrDefault();
            int num = adminInfoService.GetEntities( a => a.DelFlag == delFlagNormal && a.UName == uName && a.Id != adminInfo.Id).Count();
            if (num > 0)
            {
                Response.Write("<script>alert('该用户名已存在 ！');</script>");
                return View(adminInfoExt);
            }

            string email = adminInfoExt.Email;
            var adminInfoExts = adminInfoExtService.GetEntities(a => a.Email == email && a.DelFlag == delFlagNormal && a.Id != adminInfo.Id).ToList();
            if (adminInfoExts.Count() > 0)
            {
                Response.Write("<script>alert('该邮箱已存在 ！');</script>");
                return View(adminInfoExt);
            }

            adminInfo.UName = uName;
            adminInfo.ModifiedOn = DateTime.Now;
      
            adminInfoExt.ModifiedOn = DateTime.Now;

            if (adminInfoExtService.Update(adminInfoExt) && adminInfoService.Update(adminInfo))
            {
                //更新缓存
                var userLoginId = Request.Cookies["userLoginId"].Value;
                CacheHelper.AddCache(userLoginId, adminInfo, DateTime.Now.AddMinutes(20));
                Response.Write("<script>alert('修改成功 ！');</script>");
                return View(adminInfoExt);
            }
            Response.Write("<script>alert('修改失败，请重试 ！');</script>");
            return View(adminInfoExt);
        }

        
    }
}