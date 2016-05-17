using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZKH.Blog.Common;
using ZKH.Blog.IBLL;
using ZKH.Blog.Model;

namespace ZKH.Blog.UI.Portal.Controllers.Admin
{
    public class AdminPwdController : BaseController
    {
        IAdminInfoService adminInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(string oldPwd, string newPwd, string SecPwd)
        {
            //TODO:字段改为分大小写
            oldPwd = MD5Helper.Get32bitMD5(oldPwd, 32);
            string curPwd = adminInfoService.GetEntities(a => a.Id == this.LoginUser.Id).FirstOrDefault().Pwd;
            if (oldPwd != curPwd)
            {
                return Content("no:原密码错误 ！");
            }
            if (newPwd != SecPwd)
            {
                return Content("no:两次输入密码不匹配 ！");
            }
            newPwd = MD5Helper.Get32bitMD5(newPwd, 32);
            AdminInfo adminInfo = adminInfoService.GetEntities(a => a.Id == this.LoginUser.Id).FirstOrDefault();
            adminInfo.Pwd = newPwd;
            adminInfo.ModifiedOn = DateTime.Now;
            if (adminInfoService.Update(adminInfo))
            {
                return Content("ok:密码修改成功 ！");
            }
            return Content("no:密码修改失败 ！");
        }
    }
}