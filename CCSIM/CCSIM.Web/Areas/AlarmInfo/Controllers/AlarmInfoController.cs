using CCSIM.BLL;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.Areas.AlarmInfo.Controllers
{
    public class AlarmInfoController : Controller
    {
        // GET: AlarmInfo/AlarmInfo
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
            var endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = AlarmBLL.GetList("",-1,stTime,endTime, 1, 20, out recordCount);
            ViewBag.Grid1RecordCount = recordCount;
            ViewBag.Grid1DataSource = data;
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlarmInfoGrid_PageIndexChanged(JArray AlarmInfoGrid_fields, string objectName, int alarmType,DateTime startTime, DateTime endTime, int AlarmInfoGrid_pageIndex, int AlarmInfoGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("AlarmInfoGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var edTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = AlarmBLL.GetList(objectName,alarmType, stTime, edTime, AlarmInfoGrid_pageIndex + 1, AlarmInfoGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, AlarmInfoGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnSearch_Click(JArray AlarmInfoGrid_fields, string objectName, int alarmType, DateTime startTime, DateTime endTime, int AlarmInfoGrid_pageIndex, int AlarmInfoGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("AlarmInfoGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var edTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = AlarmBLL.GetList(objectName, alarmType, stTime, edTime, AlarmInfoGrid_pageIndex + 1, AlarmInfoGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, AlarmInfoGrid_fields);

            return UIHelper.Result();
        }
    }
}