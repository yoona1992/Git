using CCSIM.BLL;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.Areas.Message.Controllers
{
    public class MessageController : CCSIM.Web.Controllers.BaseController
    {
        // GET: Message/Message
        public ActionResult Index()
        {
            LoadData();
            return View();
        }

        #region BindGrid

        private void LoadData()
        {
            var recordCount = 0;
            var stTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = MessageBLL.GetList(new List<string>(),"", stTime, endTime, 1, 20, out recordCount);
            ViewBag.Grid1RecordCount = recordCount;
            ViewBag.Grid1DataSource = data;
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MessageGrid_PageIndexChanged(JArray MessageGrid_fields, string telephone,string title,DateTime startTime,DateTime endTime, int MessageGrid_pageIndex, int MessageGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("MessageGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var edTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var telephoneList = new List<string>();
            telephoneList.Add("15989488656");
            var data = MessageBLL.GetList(telephoneList, title, stTime, edTime, MessageGrid_pageIndex + 1, MessageGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, MessageGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnSearch_Click(JArray MessageGrid_fields, string telephone, string title, DateTime startTime, DateTime endTime, int MessageGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("MessageGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var edTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var telephoneList = new List<string>();
            telephoneList.Add("15989488656");
            var data = MessageBLL.GetList(telephoneList, title, stTime, edTime, 1, MessageGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, MessageGrid_fields);

            return UIHelper.Result();
        }

    }
}