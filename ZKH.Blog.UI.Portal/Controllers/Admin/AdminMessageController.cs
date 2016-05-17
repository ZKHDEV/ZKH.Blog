using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZKH.Blog.IBLL;
using ZKH.Blog.Model;
using ZKH.Blog.Model.Enum;

namespace ZKH.Blog.UI.Portal.Controllers.Admin
{
    public class AdminMessageController : BaseController
    {
        IMessageInfoService messageInfoService { get; set; }
        IReplyInfoService replyInfoService { get; set; }
        short delFlagNormal = (short)DelFlagEnum.Normal;

        public ActionResult Index()
        {
            ViewData["msgs"] = messageInfoService.GetEntities(a => a.DelFlag == delFlagNormal).OrderByDescending(a => a.SubTime).ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Reply(int id)
        {
            var replyList = replyInfoService.GetEntities(a => a.MessageInfoId == id && a.DelFlag == delFlagNormal).OrderByDescending(a => a.SubTime).ToList();
            ViewData["replyList"] = replyList;
            ViewData["msgId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(ReplyInfo replyInfo)
        {
            if(replyInfo.Content == null)
            {
                return Json(new { result = "no" }, JsonRequestBehavior.AllowGet);
            }
            replyInfo.DelFlag = delFlagNormal;
            replyInfo.Name = this.LoginUser.UName;
            replyInfo.SubTime = DateTime.Now;
            if (replyInfoService.Add(replyInfo) != null)
            {
                return Json(new { result = "ok", reply = replyInfo }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = "no" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DelRep(int id)
        {
            List<int> ids = new List<int>();
            ids.Add(id);
            if (replyInfoService.DeleteListByLogical(ids))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult Delete(string ids)
        {
            string[] strIds = ids.TrimEnd(',').Split(',');
            List<int> dellist = new List<int>();
            for (int i = 0; i < strIds.Length; i++)
            {
                dellist.Add(Int32.Parse(strIds[i]));
            }

            if (messageInfoService.DeleteListByLogical(dellist))
            {
                List<int> cids = replyInfoService.GetEntities(a => dellist.Contains(a.MessageInfoId) && a.DelFlag == delFlagNormal).Select(a => a.Id).ToList();
                replyInfoService.DeleteListByLogical(cids);
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
    }
}