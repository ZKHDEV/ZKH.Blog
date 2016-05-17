using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZKH.Blog.IBLL;

namespace ZKH.Blog.UI.Portal.Controllers.Admin
{
    public class AdminController : BaseController
    {
        IWebInfoService webInfoService { get; set; }

        public ActionResult Index()
        {
            var webInfo = webInfoService.GetEntities(a => true).FirstOrDefault();
            if (webInfo != null)
            {
                ViewData["webName"] = webInfo.Name;
            }
            else
            {
                ViewData["webName"] = "ZKH.Blog";
            }
            ViewData.Model = this.LoginUser;
            return View();
        }

        public ActionResult Menu()
        {
            ViewData.Model = this.LoginUser;
            return View();
        }
    }
}