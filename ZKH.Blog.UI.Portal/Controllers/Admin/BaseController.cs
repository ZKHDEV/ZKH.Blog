using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZKH.Blog.Common.Cache;
using ZKH.Blog.Model;
using ZKH.Blog.Model.Enum;

namespace ZKH.Blog.UI.Portal.Controllers.Admin
{
    public class BaseController : Controller
    {
        public bool IsCheckLogin = true;
        public AdminInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (IsCheckLogin)
            {
                HttpCookie cookie = Request.Cookies["userLoginId"];
                if (cookie == null)
                {
                    filterContext.HttpContext.Response.Redirect("/Account/Login");
                    return;
                }

                string userGuid = cookie.Value;
                AdminInfo adminInfo = CacheHelper.GetCache(userGuid) as AdminInfo;
                if (adminInfo == null)
                {
                    filterContext.HttpContext.Response.Redirect("/Account/Login");
                    return;
                }

                LoginUser = adminInfo;
                CacheHelper.SetCache(userGuid, adminInfo, DateTime.Now.AddMinutes(20));               
            }
        }
    }
}