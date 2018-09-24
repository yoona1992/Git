using CCSIM.BLL;
using CCSIM.Web.App_Start;
using CCSIM.Web.Models;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.Areas.Notification.Controllers
{
    public class NotificationController : CCSIM.Web.Controllers.BaseController
    {
        // GET: Message/Message
        public ActionResult Index()
        {
            LoadData();
            return View();
        }

        public ActionResult Add()
        {
            ViewData["phone"] = Request.QueryString["phone"];
            return View();
        }

        #region BindGrid

        private void LoadData()
        {
            var recordCount = 0;
            var stTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = NotificationBLL.GetList("", "", stTime, endTime, 1, 20, out recordCount);
            ViewBag.Grid1RecordCount = recordCount;
            ViewBag.Grid1DataSource = data;
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NotificationGrid_PageIndexChanged(JArray NotificationGrid_fields, string username, string title, DateTime startTime, DateTime endTime, int NotificationGrid_pageIndex, int NotificationGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("NotificationGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var edTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = NotificationBLL.GetList(username, title, stTime, edTime, NotificationGrid_pageIndex + 1, NotificationGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, NotificationGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "Notification/btnSearch_Click", Description = "通知查询")]
        public ActionResult btnSearch_Click(JArray NotificationGrid_fields, string username, string title, DateTime startTime, DateTime endTime, int NotificationGrid_pageIndex, int NotificationGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("NotificationGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var edTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = NotificationBLL.GetList(username, title, stTime, edTime, NotificationGrid_pageIndex + 1, NotificationGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, NotificationGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggerFilter(Key = "Notification/btnAdd_Click", Description = "通知添加")]
        public ActionResult btnAdd_Click(FormCollection values)
        {
            var msg = NotificationBLL.SendMessage(values["Phone"], values["Title"], values["Content"]);
            ActiveWindow.HidePostBack();
            ShowNotify(msg);
            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Window1_Close()
        {
            // 调用父页面定义的函数 reload
            PageContext.RegisterStartupScript("reload();");

            return UIHelper.Result();
        }

        public ActionResult GetUserList()
        {
            var data = UserBLL.GetUserList();

            return new JsonResult
            {
                Data = data
            };
        }

        [HttpPost]
        [LoggerFilter(Key = "Notification/SendNotification", Description = "通知发送")]
        public ActionResult SendNotification(string phone, string title, string content)
        {
            var msg = NotificationBLL.SendMessage(phone, title, content);

            return new JsonResult
            {
                Data = msg
            };
        }


    }
}