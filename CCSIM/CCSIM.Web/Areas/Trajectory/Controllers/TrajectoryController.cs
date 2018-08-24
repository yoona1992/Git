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

        public ActionResult Detail_User()
        {
            LoadData_DetailUser();
            return View();
        }

        public ActionResult Detail_Car()
        {
            //LoadData_DetailCar();
            return View();
        }

        public ActionResult Map_User()
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

            var recordCount1 = 0;
            var data1 = CarBLL.GetList("", 1, 20, out recordCount1);
            ViewBag.CarGridRecordCount = recordCount1;
            ViewBag.CarGridDataSource = data1;
        }

        private void LoadData_DetailUser()
        {
            var telephone = Request.QueryString["telephone"];
            var recordCount = 0;
            var stTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = TrajectoryBLL.GetList_User(telephone, stTime, endTime, 1, 20, out recordCount);
            ViewBag.TrajectoryDetailUserGridRecordCount = recordCount;
            ViewBag.TrajectoryDetailUserGridDataSource = data;
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
        public ActionResult CarGrid_PageIndexChanged(JArray CarGrid_fields, string vehicleNo, int CarGrid_pageIndex, int CarGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("CarGrid");
            var recordCount = 0;
            var data = CarBLL.GetList(vehicleNo, CarGrid_pageIndex + 1, CarGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, CarGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrajectoryDetailUserGrid_PageIndexChanged(JArray TrajectoryDetailUserGrid_fields, string telephone, DateTime passTime, int TrajectoryDetailUserGrid_pageIndex, int TrajectoryDetailUserGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("TrajectoryDetailUserGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(passTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime= DateTime.Parse(passTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = TrajectoryBLL.GetList_User(telephone, stTime, endTime, TrajectoryDetailUserGrid_pageIndex + 1, TrajectoryDetailUserGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, TrajectoryDetailUserGrid_fields);

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
        public ActionResult btnCarSearch_Click(JArray CarGrid_fields, string vehicleNo, int CarGrid_pageIndex, int CarGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("CarGrid");
            var recordCount = 0;
            var data = CarBLL.GetList(vehicleNo, CarGrid_pageIndex + 1, CarGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, CarGrid_fields);

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnDetailUserSearch_Click(JArray TrajectoryDetailUserGrid_fields, string telephone,DateTime passTime, int TrajectoryDetailUserGrid_pageIndex, int TrajectoryDetailUserGrid_pageSize)
        {
            var grid1 = UIHelper.Grid("TrajectoryDetailUserGrid");
            var recordCount = 0;
            var stTime = DateTime.Parse(passTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime = DateTime.Parse(passTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = TrajectoryBLL.GetList_User(telephone, stTime, endTime, TrajectoryDetailUserGrid_pageIndex + 1, TrajectoryDetailUserGrid_pageSize, out recordCount);

            grid1.RecordCount(recordCount);
            grid1.DataSource(data, TrajectoryDetailUserGrid_fields);

            return UIHelper.Result();
        }
        
        public ActionResult GetList_User(string telephone,DateTime passTime)
        {
            var stTime = DateTime.Parse(passTime.ToString("yyyy-MM-dd") + " 00:00:00");
            var endTime = DateTime.Parse(passTime.ToString("yyyy-MM-dd") + " 23:59:59");
            var data = TrajectoryBLL.GetList_User(telephone,stTime,endTime);

            return new JsonResult
            {
                Data = data
            };
        }

    }
}