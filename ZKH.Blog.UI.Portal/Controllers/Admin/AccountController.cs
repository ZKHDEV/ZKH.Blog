using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZKH.Blog.Common;
using ZKH.Blog.Common.Cache;
using ZKH.Blog.IBLL;
using ZKH.Blog.Model;
using ZKH.Blog.Model.Enum;

namespace ZKH.Blog.UI.Portal.Controllers.Admin
{
    public class AccountController : Controller
    {
        IAdminInfoService adminInfoService { get; set; }
        IAdminInfoExtService adminInfoExtService { get; set; }
        short delFlagNormal = (short)DelFlagEnum.Normal;

        public ActionResult GetValidateCode()
        {
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(6);
            Session["vCode"] = code;
            byte[] buff = validateCode.CreateValidateGraphic(code);
            return File(buff, "image/jpeg");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UName, string Pwd, string vCode)
        {
            if (UName == null || Pwd == null)
            {
                return Content("no:用户名或密码不能为空 !");
            }
            if (vCode == null)
            {
                return Content("no:验证码不能为空 !");
            }
            string validateCode = Session["vCode"].ToString();
            if (vCode != validateCode)
            {
                return Content("no:验证码错误 !");
            }

            AdminInfo userInfo = adminInfoService.GetEntities(u => u.UName == UName && u.Pwd.Equals(Pwd,StringComparison.CurrentCulture) && u.DelFlag == (short)delFlagNormal).FirstOrDefault();

            if (userInfo != null)
            {
                string userLoginId = Guid.NewGuid().ToString();
                Response.Cookies["userLoginId"].Value = userLoginId;
                CacheHelper.AddCache(userLoginId, userInfo, DateTime.Now.AddMinutes(20));

                return Content("ok:登陆成功 !");
            }
            return Content("no:用户名或密码错误 !");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AdminInfoExt adminInfoExt)
        {
            if (!ModelState.IsValid)
            {
                return View(adminInfoExt);
            }

            string uName = Request["UName"] == null ? string.Empty : Request["UName"].ToString();
            string pwd1 = Request["Pwd1"] == null ? string.Empty : Request["Pwd1"].ToString();
            string pwd2 = Request["Pwd2"] == null ? string.Empty : Request["Pwd2"].ToString();
            if (string.IsNullOrEmpty(uName) || string.IsNullOrEmpty(pwd1) || string.IsNullOrEmpty(pwd2))
            {
                Response.Write("<script>alert('用户名或密码不能为空 ！');</script>");
                return View(adminInfoExt);
            }
            if (adminInfoService.GetEntities(a => a.UName == uName).Count() > 0)
            {
                Response.Write("<script>alert('该用户名已存在 ！');</script>");
                return View(adminInfoExt);
            }
            if (pwd1 != pwd2)
            {
                Response.Write("<script>alert('两次密码输入不匹配 ！');</script>");
                return View(adminInfoExt);
            }

            string email = adminInfoExt.Email;
            var adminInfoExts = adminInfoExtService.GetEntities(a => a.Email == email && a.DelFlag == delFlagNormal).ToList();
            if (adminInfoExts.Count() > 0)
            {
                Response.Write("<script>alert('该邮箱已被注册 ！');</script>");
                return View(adminInfoExt);
            }

            AdminInfo adminInfo = new AdminInfo();
            adminInfo.UName = uName;
            adminInfo.Pwd = MD5Helper.Get32bitMD5(pwd1, 32);
            adminInfo.ModifiedOn = DateTime.Now;
            adminInfo.SubTime = DateTime.Now;
            adminInfo.DelFlag = delFlagNormal;
            adminInfo.IsSuperUser = false;
            adminInfo = adminInfoService.Add(adminInfo);

            adminInfoExt.ModifiedOn = DateTime.Now;
            adminInfoExt.SubTime = DateTime.Now;
            adminInfoExt.DelFlag = delFlagNormal;
            adminInfoExt.AdminInfoId = adminInfo.Id;
            adminInfoExt = adminInfoExtService.Add(adminInfoExt);
            if (adminInfo != null && adminInfoExt != null)
            {
                Response.Write("<script>alert('创建成功 ！');</script>");
                return RedirectToAction("Login");
            }
            Response.Write("<script>alert('创建失败，请重试 ！');</script>");
            return View(adminInfoExt);
        }

        public ActionResult Exit()
        {
            Response.Cookies["userLoginId"].Value = null;
            return Content("ok");
        }

        [HttpGet]
        public ActionResult Forget()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forget(string Email, string vCode)
        {
            if (Email == null || vCode == null)
            {
                return Content("no:邮箱和验证码不能为空 !");
            }
            string validateCode = Session["vCode"].ToString();
            if (vCode != validateCode)
            {
                return Content("no:验证码错误 !");
            }

            var adminInfoExt = adminInfoExtService.GetEntities(a => a.Email == Email && a.DelFlag == delFlagNormal).FirstOrDefault();
            if (adminInfoExt == null)
            {
                return Content("no:用户不存在 !");
            }

            Random rd = new Random();
            string newPassword = ((char)(rd.Next(0, 26) + 96)).ToString() + ((char)(rd.Next(0, 26) + 96)).ToString() + rd.Next(0, 9).ToString() + rd.Next(0, 9).ToString() + rd.Next(0, 9).ToString() + rd.Next(0, 9).ToString() + rd.Next(0, 9).ToString();
            string pwd = MD5Helper.Get32bitMD5(newPassword, 32);
            var adminInfo = adminInfoService.GetEntities(a => a.Id == adminInfoExt.AdminInfoId).FirstOrDefault();
            adminInfo.Pwd = pwd;
            if (adminInfoService.Update(adminInfo))
            {
                if(!EmailHelper.SendEmail(Email, "ZKH.Blog账户密码重置", "您好，您的临时密码为：" + newPassword + "，请尽快登录并修改密码！"))
                {
                    return Content("ok:重置邮件发送失败，请重试 ！");
                }
                return Content("ok:密码重置成功，请登录邮箱查看 ！");
            }
            else
            {
                return Content("no:密码重置失败，请重试 ！");
            }
        }
    }
}