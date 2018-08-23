using CCSIM.BLL;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCSIM.Web.Areas.Trajectory.Controllers
{
    public class TrajectoryController : Controller
    {
        // GET: Trajectory/Trajectory
        public ActionResult Index()
        {
            LoadData();
            return View();
        }
        public ActionResult Detail()
        {
            LoadData_Detail();
            return View();
        }

        public ActionResult Map()
        {
            return View();
        }


        #region BindGrid

        private void LoadData()
        {
            var recordCount = 0;
            var data = UserBLL.GetList("", "", -1, 1, 20, out recordCount);
            ViewBag.UserGridRecordCount = recordCount;
            ViewBag.UserGridDataSource = data;
        }

        private void LoadData_Detail()
        {
            var telephone = Request.QueryString["telephone"];
            var recordCount = 0;
            var stTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = TrajectoryBLL.GetList(telephone, stTime, endTime, 1, 20, out recordCount);
            ViewBag.TrajectoryDetailGridRecordCount = recordCount;
            ViewBag.TrajectoryDetailGridDataSource = data;
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserGrid_PageIndexChanged(JArray UserGrid_fields, string userName, string telephone, int UserGrid_pageIndex, int UserGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("UserGrid");
            var recordCount = 0;
            var data = UserBLL.GetList(userName, telephone, -1, UserGrid_pageIndex + 1, UserGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, UserGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrajectoryDetailGrid_PageIndexChanged(JArray TrajectoryDetailGrid_fields, string telephone, DateTime passTime, int TrajectoryDetailGrid_pageIndex, int TrajectoryDetailGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("TrajectoryDetailGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(passTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime= DateTime.Parse(passTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = TrajectoryBLL.GetList(telephone, stTime, endTime, TrajectoryDetailGrid_pageIndex + 1, TrajectoryDetailGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, TrajectoryDetailGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnUserSearch_Click(JArray UserGrid_fields, string userName, string telephone, int UserGrid_pageIndex, int UserGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("UserGrid");
            var recordCount = 0;
            var data = UserBLL.GetList(userName, telephone, -1, UserGrid_pageIndex+ 1, UserGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, UserGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnSearch_Click(JArray TrajectoryDetailGrid_fields, string telephone,DateTime passTime, int TrajectoryDetailGrid_pageIndex, int TrajectoryDetailGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("TrajectoryDetailGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(passTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime = DateTime.Parse(passTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = TrajectoryBLL.GetList(telephone, stTime, endTime, TrajectoryDetailGrid_pageIndex+1, TrajectoryDetailGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, TrajectoryDetailGrid_fields);

            return UIHelper.Result();
        }
        
        public ActionResult GetList(string telephone,DateTime passTime)
        {
            var stTime = DateTime.Parse(passTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime = DateTime.Parse(passTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = TrajectoryBLL.GetList(telephone,stTime,endTime);

            return new JsonResult
            {
                Data = data
            };
        }

    }
}